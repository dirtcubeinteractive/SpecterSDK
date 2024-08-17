using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.API
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

        public async Task WaitForTokenAsync()
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
            Func<HttpResponseMessage, SPApiResponse<TData>> handleResponse,
            CancellationToken cancellationToken = default) where TData : class, ISpecterApiResponseData, new()
        {
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

                    // If the response does not require a retry, return the result
                    if (!ShouldRetry(response.StatusCode))
                    {
                        return handleResponse(response);
                    }
                }
                catch (Exception e)
                {
                    // Handle client-side exceptions
                    if (attempt >= m_MaxRetries)
                    {
                        return CreateErrorResponse<TData>(500, "Max retries exceeded", e.Message);
                    }
                    
                    SPDebug.LogError($"SP Client Side Error for endpoint {endpoint}: {e.ToString()}");
                }
                finally
                {
                    Semaphore.Release();
                }

                attempt++;
                if (attempt < m_MaxRetries)
                {
                    await Task.Delay(delayTime, cancellationToken);
                    delayTime = TimeSpan.FromMilliseconds(delayTime.TotalMilliseconds * 2); // Exponential backoff
                }
            } while (attempt < m_MaxRetries);

            return CreateErrorResponse<TData>(503, "Max retries exceeded", "Service unavailable after multiple retry attempts.");
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

        private SPApiResponse<TData> CreateErrorResponse<TData>(int statusCode, string status, string errorMessage)
            where TData : class, ISpecterApiResponseData, new()
        {
            return new SPApiResponse<TData>
            {
                status = SPApiStatus.Error,
                code = statusCode,
                message = status,
                errors = new List<SPApiError>
                {
                    new SPApiError
                    {
                        code = statusCode,
                        status = status,
                        errorCode = statusCode,
                        message = status,
                        errorMessage = errorMessage
                    }
                },
                data = null
            };
        }
    }
}