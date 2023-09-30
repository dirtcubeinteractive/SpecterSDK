using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;
using AuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace SpecterSDK.API
{
    public struct SPApiStatus
    {
        public const string Success = "success";
        public const string Error = "error";
        public const string Warning = "warning";
        public const string Pending = "pending";
        public const string Incomplete = "incomplete";
        public const string UnprocessableEntity = "unprocessable_entity";
    }

    public enum SPAuthType
    {
        None,
        AccessToken
    }
    
    public struct SPApiAuthScheme
    {
        public const string Bearer = "Bearer";
    }

    public struct SPApiMediaType
    {
        public const string ApplicationJson = "application/json";
    }

    public abstract class SpecterApiClientBase
    {
        private static HttpClient m_HttpClient;
        
        protected readonly SpecterRuntimeConfig m_Config;

        public virtual SPAuthType AuthType => SPAuthType.None;
        
        protected SpecterApiClientBase(SpecterRuntimeConfig config)
        {
            m_Config = config;
            m_HttpClient ??= new HttpClient();
        }

        private void ConfigureProjectId(IProjectConfigurable request)
        {
            if (string.IsNullOrEmpty(request.projectId))
            {
                if (string.IsNullOrEmpty(m_Config?.ProjectId))
                    throw new ArgumentException($"{request.GetType().Name}: Project Id cannot be null or empty");
                
                request.projectId = m_Config.ProjectId;
            }
        }

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
            
            var suffix = requestParams != null ? $"?{SpecterJson.ToQueryString(requestParams)}" : "";
            var uri = $"{m_Config.BaseUrl}{endpoint}{suffix}";
            
            using var request = new HttpRequestMessage(method, uri);
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
            }

            try
            {
                Debug.Log("SP HTTP Request: " + uri);
                var response = await m_HttpClient.SendAsync(request, new CancellationToken());

                if (!response.IsSuccessStatusCode)
                {
                    var errResString = await response.Content.ReadAsStringAsync();
                    Debug.LogError(errResString);
                    
                    var apiError = SpecterJson.DeserializeObject<SPApiError>(errResString);
                    var errResponse = new SPApiResponse<TData>()
                    {
                        status = SPApiStatus.Error, 
                        errors = new List<SPApiError>() { apiError },
                        code = apiError.statusCode,
                        message = apiError.message,
                        data = null
                    };

                    return errResponse;
                }

                var resString = await response.Content.ReadAsStringAsync();
                Debug.Log(resString);
                
                var apiResponse = SpecterJson.DeserializeObject<SPApiResponse<TData>>(resString);
                return apiResponse;
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                
                const string message = "An unexpected exception occured";
                var errResponse = new SPApiResponse<TData>()
                {
                    status = SPApiStatus.Error,
                    code = 500,
                    message = message,
                    errors = new List<SPApiError>()
                    {
                        new()
                        {
                            statusCode = 500,
                            message = message,
                            error = e.Message
                        }
                    },
                    data = null
                };
                
                return errResponse;
            }
        }

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

        protected virtual TResult BuildResult<TResult, TData>(SPApiResponse<TData> response)
        where TData: class, ISpecterApiResponseData, new()
        where TResult: SpecterApiResultBase<TData>
        {
            TResult result = (TResult)Activator.CreateInstance(typeof(TResult));
            result.Response = response;
            result.InitSpecterObjects(result.LoadObjectsOnResponse);
            return result;
        }

        protected async Task<TResult> GetAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBaseData queryParams) 
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Get, endpoint, authType: authType, requestParams: queryParams);
        }

        protected async Task<TResult> PostAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBaseData bodyParams)
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Post, endpoint, authType: authType, requestBody: bodyParams);
        }

        protected async Task<TResult> PutAsync<TResult, TData>(string endpoint, SPAuthType authType, SPApiRequestBaseData bodyParams)
            where TData: class, ISpecterApiResponseData, new()
            where TResult: SpecterApiResultBase<TData>, new()
        {
            return await MakeRequestAsync<TResult, TData>(HttpMethod.Put, endpoint, authType: authType, requestBody: bodyParams);
        }
    }
}
