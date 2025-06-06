using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.Shared.Networking
{
    public class SPApiRateHandler
    {
        // Token Bucket for rate limiting
        private readonly int m_MaxRetries;
        private readonly int m_MaxTokens;
        private readonly int m_BaseRetryMillis;

        private readonly TimeSpan m_RefillInterval;
        private DateTime m_LastRefillTime = DateTime.UtcNow;

        // Lock object for token bucket operations
        private readonly object m_TokenBucketLock = new object();

        private int m_AvailableTokens;

        public SemaphoreSlim Semaphore { get; private set; }

        public SPApiRateHandler(SpecterRuntimeConfig config)
        {
            Semaphore = new SemaphoreSlim(config.MaxConcurrentRequests);
            m_MaxTokens = config.MaxTokens;
            m_MaxRetries = config.MaxRetries;
            m_RefillInterval = TimeSpan.FromMilliseconds(config.TokenRefillMillis);
            m_AvailableTokens = m_MaxTokens;
            m_BaseRetryMillis = config.BaseRetryDelayMillis;
        }

        private async Task WaitForTokenAsync()
        {
            lock (m_TokenBucketLock)
            {
                RefillTokens();
                if (m_AvailableTokens > 0)
                {
                    m_AvailableTokens--;
                    return;
                }
            }

            // If no tokens are available, wait until a token is available
            while (true)
            {
                await Task.Delay(m_RefillInterval);
                lock (m_TokenBucketLock)
                {
                    RefillTokens();
                    if (m_AvailableTokens > 0)
                    {
                        m_AvailableTokens--;
                        return;
                    }
                }
            }
        }

        private void RefillTokens()
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan delta = now - m_LastRefillTime;

            if (delta >= m_RefillInterval)
            {
                int tokensToAdd = (int)(delta.TotalMilliseconds * m_MaxTokens / m_RefillInterval.TotalMilliseconds);
                m_AvailableTokens = Math.Min(m_AvailableTokens + tokensToAdd, m_MaxTokens);
                m_LastRefillTime = now;
            }
        }

        public async Task<SPApiResponse<TData>> ExecuteWithRetryAsync<TData>(
            string endpoint,
            Func<CancellationToken, Task<HttpResponseMessage>> httpRequest,
            Func<HttpResponseMessage, Task<SPApiResponse<TData>>> handleHttpResponse,
            CancellationToken cancellationToken = default) where TData : class, ISpecterApiResponseData, new()
        {
            var startTime = DateTime.UtcNow;
            await WaitForTokenAsync();
            
            int attempt = 0;
            TimeSpan delayTime = TimeSpan.FromMilliseconds(m_BaseRetryMillis);

            do
            {
                try
                {
                    await Semaphore.WaitAsync(cancellationToken);

                    // Execute the HTTP request
                    var response = await httpRequest(cancellationToken);
                    var apiResponse = await handleHttpResponse(response);

                    // If the response does not require a retry, return the result
                    if (!ShouldRetry(response.StatusCode))
                    {
                        // Log API time even if a non-retryable error occurred
                        LogApiTime(startTime, endpoint, attempt + 1, response.IsSuccessStatusCode);
                        return apiResponse;
                    }
                }
                catch (JsonSerializationException e)
                {
                    SPDebug.LogError($"SP Error Deserializing Response for endpoint {endpoint}: {e.ToString()}");
                    return CreateErrorResponse<TData>(500, 500, "JsonSerializationException", "Json Deserialization Exception", e.Message);

                }
                catch (OperationCanceledException e) when (cancellationToken.IsCancellationRequested)
                {
                    SPDebug.LogWarning($"SP Request Cancelled By Client for endpoint {endpoint}");
                    return CreateErrorResponse<TData>(499, 499, "ClientClosedRequest", "Cancellation token was triggered", e.Message);
                }
                catch (Exception e)
                {
                    SPDebug.LogError($"SP Client Side Error for endpoint {endpoint}: {e.ToString()}");
                    return CreateErrorResponse<TData>(500, 500, "InternalClientException", "An unexpected client exception occurred", e.Message);
                }
                finally
                {
                    Semaphore.Release();
                }

                attempt++;
                if (attempt < m_MaxRetries)
                {
                    SPDebug.Log($"SP HTTP Request for endpoint {endpoint} retrying {m_MaxRetries - attempt} more times after {delayTime.TotalMilliseconds} millis...");
                    await Task.Delay(delayTime, cancellationToken);
                    delayTime = TimeSpan.FromMilliseconds(delayTime.TotalMilliseconds * 2); // Exponential backoff
                }
            } while (attempt < m_MaxRetries);

            // Log API time for total time taken once retries have been exhausted
            LogApiTime(startTime, endpoint, attempt, false);
            return CreateErrorResponse<TData>(503, 503, "RetriesExceededException","Max retries exceeded", "Service unavailable after multiple retry attempts.");
        }

        private bool ShouldRetry(HttpStatusCode code)
        {
            return code switch
            {
                HttpStatusCode.BadGateway => true,
                HttpStatusCode.RequestTimeout => true,
                HttpStatusCode.GatewayTimeout => true,
                HttpStatusCode.InternalServerError => true,
                HttpStatusCode.ServiceUnavailable => true,
                _ => false,
            };
        }

        private void LogApiTime(DateTime startTime, string endpoint, int attempts, bool success)
        {
            var time = DateTime.UtcNow - startTime;
            SPDebug.Log($"SP Api Call for endpoint {endpoint} completed {(success ? "successfully" : "with error")} after total {time.TotalMilliseconds} millis and {attempts} attempts.");
        }

        private SPApiResponse<TData> CreateErrorResponse<TData>(int statusCode, int errorCode, string statusMessage, string message, string errorMessage)
            where TData : class, ISpecterApiResponseData, new()
        {
            return new SPApiResponse<TData>
            {
                status = SPApiStatus.Error,
                code = statusCode,
                message = statusMessage,
                errors = new List<SPApiError>
                {
                    new SPApiError
                    {
                        code = statusCode,
                        status = statusMessage,
                        message = message,
                        errorCode = errorCode,
                        errorMessage = errorMessage
                    }
                },
                data = null
            };
        }
    }
}