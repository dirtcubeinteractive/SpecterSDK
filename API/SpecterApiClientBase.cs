using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;
using AuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace SpecterSDK.API
{
    /// <summary>
    /// Enumeration representing the types of authentication used for API requests.
    /// </summary>
    public enum SPAuthType
    {
        None,
        AccessToken
    }
    
    /// <summary>
    /// Constants representing possible authentication schemes used in API requests.
    /// </summary>
    public struct SPApiAuthScheme
    {
        public const string Bearer = "Bearer";
        public const string ApiKey = "Api-Key";
    }

    /// <summary>
    /// Constants representing possible MIME types in API calls
    /// </summary>
    public struct SPApiMediaType
    {
        public const string ApplicationJson = "application/json";
    }

    /// <summary>
    /// The foundational class for all API interactions in the Specter SDK.
    /// It abstracts away the common logic and patterns used in making API requests, ensuring a consistent approach and reducing redundancy.
    /// All API modules in Specter derive from this class.
    /// </summary>
    public abstract class SpecterApiClientBase
    {
        // Shared HTTP client instance for making API requests.
        private static HttpClient m_HttpClient;
        
        // Configuration settings for the Specter runtime.
        protected readonly SpecterRuntimeConfig m_Config;

        // Default authentication type for API requests. Can be overridden in derived classes.
        public virtual SPAuthType AuthType => SPAuthType.None;
        
        // Constructor that initializes the API client with the provided configuration settings.
        protected SpecterApiClientBase(SpecterRuntimeConfig config)
        {
            m_Config = config;
            
            // Initialize the shared Http client only if another API client has not already done so.
            m_HttpClient ??= new HttpClient();
        }

        // Ensures that the project ID is always set for requests that require it.
        private void ConfigureProjectId(IProjectConfigurable request)
        {
            if (string.IsNullOrEmpty(request.projectId))
            {
                if (string.IsNullOrEmpty(m_Config?.ProjectId))
                    throw new ArgumentException($"{request.GetType().Name}: Project Id cannot be null or empty");
                
                request.projectId = m_Config.ProjectId;
            }
        }

        /// <summary>
        /// Core method to make API requests. It handles the entire lifecycle of a request, from constructing the URL to handling various response scenarios.
        /// </summary>
        /// <param name="method">Http method to use, i.e. GET, POST, etc.</param>
        /// <param name="endpoint">Url endpoint appended to the configured base Url</param>
        /// <param name="authType">Authentication mechanism to be set in the request</param>
        /// <param name="requestParams">Query parameters - only applicable in GET requests</param>
        /// <param name="requestBody">Body of the request</param>
        /// <typeparam name="TData">Type of data returned in the <see cref="SPApiResponse{T}"/></typeparam>
        /// <returns>A deserialized instance of <see cref="SPApiResponse{T}"/></returns>
        private async Task<SPApiResponse<TData>> MakeRequestAsync<TData>(
            HttpMethod method,
            string endpoint = "",
            SPAuthType authType = SPAuthType.None,
            object requestParams = null,
            object requestBody = null
            )
        where TData: class, ISpecterApiResponseData, new()
        {
            if (m_Config == null || m_HttpClient == null)
            {
                throw new OperationCanceledException("Specter was not initialized correctly. Please call Specter.Initialize or set Specter Config to AutoInit");
            }
            
            if (requestParams is IProjectConfigurable paramProjConfig)
                ConfigureProjectId(paramProjConfig);
            
            if (requestBody is IProjectConfigurable bodyProjConfig)
                ConfigureProjectId(bodyProjConfig);

            var uri = $"{m_Config.BaseUrl}{endpoint}";
            if (requestParams != null)
            {
                var query = $"?{SpecterJson.ToQueryString(requestParams)}";
                uri  += query;
                Debug.Log("SP HTTP Request Query Params: " + query);
            }

            using var request = new HttpRequestMessage(method, uri);
            request.Headers.Add(SPApiAuthScheme.ApiKey, m_Config.ApiKey);
            
            if (string.IsNullOrEmpty(m_Config.ApiKey))
                Debug.LogError("Specter Error: API Key is null or empty");
            
            switch (authType)
            {
                case SPAuthType.AccessToken:
                    request.Headers.Authorization = new AuthenticationHeaderValue(SPApiAuthScheme.Bearer, m_Config.AccessToken);
                    break;
                case SPAuthType.None:
                    break;
                default:
                    throw new NotImplementedException($"Specter SDK unhandled auth type: {authType.ToString()}");
            }
            
            if (requestBody != null)
            {
                var bodyStr = SpecterJson.SerializeObject(requestBody);
                request.Content = new StringContent(bodyStr, Encoding.UTF8, SPApiMediaType.ApplicationJson);
                Debug.Log($"SP HTTP Request Payload for endpoint {endpoint}: " + bodyStr);
            }

            try
            {
                Debug.Log("SP HTTP Request Full URL: " + uri);
                var response = await m_HttpClient.SendAsync(request, new CancellationToken());

                if (!response.IsSuccessStatusCode)
                {
                    var errResString = await response.Content.ReadAsStringAsync();
                    Debug.LogError($"SP Api Error for endpoint {endpoint}: {errResString}");
                    
                    var apiError = SpecterJson.DeserializeObject<SPApiError>(errResString);
                    var errResponse = new SPApiResponse<TData>()
                    {
                        status = SPApiStatus.Error, 
                        errors = new List<SPApiError>() { apiError },
                        code = apiError.errorCode,
                        message = apiError.message,
                        data = null
                    };

                    return errResponse;
                }

                var resString = await response.Content.ReadAsStringAsync();
                Debug.Log($"SP HTTP Response for Endpoint {endpoint}: {resString}");
                
                var apiResponse = SpecterJson.DeserializeObject<SPApiResponse<TData>>(resString);
                return apiResponse;
            }
            catch (Exception e)
            {
                if (e is JsonSerializationException jsonException)
                {
                    Debug.LogError($"SP Error Deserializing Response for endpoint {endpoint}: {e.ToString()}");
                }
                else
                    Debug.LogError($"SP Client Side Error for endpoint {endpoint}: {e.ToString()}");
                
                const string message = "An unexpected client side exception occured while making the request ";
                var errResponse = new SPApiResponse<TData>()
                {
                    status = SPApiStatus.Error,
                    code = 500,
                    message = message,
                    errors = new List<SPApiError>()
                    {
                        new()
                        {
                            code = 500,
                            status = "InternalClientException",
                            errorCode = 500,
                            message = message,
                            errorMessage = e.Message
                        }
                    },
                    data = null
                };
                
                return errResponse;
            }
        }

        /// <summary>
        /// An extension of the core request method that further processes the response to return a more specific result type.
        /// This allows for more specialized post-processing based on the type of request.
        /// </summary>
        /// <returns>A processed instance of <see cref="SpecterApiResultBase{T}"/></returns>
        private async Task<TResult> MakeRequestAsync<TResult, TData>(
            HttpMethod method, 
            string endpoint = "", 
            SPAuthType authType = SPAuthType.None,
            object requestParams = null, 
            object requestBody = null
            ) 
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            var response = await MakeRequestAsync<TData>(method, endpoint, authType, requestParams, requestBody);
            return BuildResult<TResult, TData>(response);
        }

        /// <summary>
        /// Transforms the Specter API response into a game ready result object.
        /// This provides a consistent way to handle and interpret API responses across the SDK.
        /// </summary>
        /// <param name="response">Raw API response</param>
        /// <typeparam name="TResult">Type of result - a subclass of <see cref="SpecterApiResultBase{T}"/></typeparam>
        /// <typeparam name="TData">Type of data contained within the API response</typeparam>
        /// <returns>Constructed API result object</returns>
        protected virtual TResult BuildResult<TResult, TData>(SPApiResponse<TData> response)
        where TData: class, ISpecterApiResponseData, new()
        where TResult: SpecterApiResultBase<TData>
        {
            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));
            result.Response = response;
            result.InitSpecterObjects(result.LoadObjectsOnResponse);
            return result;
        }

        // Convenience methods for making specific types of HTTP requests. These abstract away the HTTP verb details, providing a more semantic way to make requests.
        protected async Task<TResult> GetAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBase queryParams) 
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Get, endpoint, authType: authType, requestParams: queryParams);
        }

        protected async Task<TResult> PostAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBase bodyParams)
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Post, endpoint, authType: authType, requestBody: bodyParams);
        }

        protected async Task<TResult> PutAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBase bodyParams)
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Put, endpoint, authType: authType, requestBody: bodyParams);
        }
    }
}
