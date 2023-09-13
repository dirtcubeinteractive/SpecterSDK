using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpecterSDK.Shared;
using UnityEngine;

using AuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace SpecterSDK.APIClients
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

    public enum SPEnvironment
    {
        Development,
        Staging,
        Production
    }

    [System.Serializable]
    public class SPApiRequestEntity
    {
        public string value { get; set; }
        public List<string> attributes { get; set; }
        public int limit { get; set; } = 1;
        public int? offset { get; set; } = 0;
    }

    [System.Serializable]
    public class SPApiResponse<T> where T: class
    {
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<SPApiError> errors { get; set; }
        public T data { get; set; }
    }

    [System.Serializable]
    public class SPApiError
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string error { get; set; }
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
        
        public static string ToQueryString(object obj, string prefix = null)
        {
            if (obj == null)
                return string.Empty;

            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select ToQueryStringProperty(p.Name, p.GetValue(obj, null), prefix);

            return string.Join("&", properties.Where(p => !string.IsNullOrEmpty(p)));
        }

        protected static string ToQueryStringProperty(string name, object value, string prefix)
        {
            string key = string.IsNullOrEmpty(prefix) ? Uri.EscapeDataString(name) : $"{prefix}.{Uri.EscapeDataString(name)}";

            if (value is string or ValueType) // Check if it's a value type or string
            {
                return $"{key}={Uri.EscapeDataString(value.ToString())}";
            }

            // If nested object, recursively call to serialize
            return ToQueryString(value, key);
        }

        public async Task<SPApiResponse<T>> MakeRequestAsync<T>(
            HttpMethod method, 
            string endpoint = "", 
            SPAuthType authType = SPAuthType.None,
            object requestParams = null, 
            object requestBody = null
            ) where T: class
        {
            if (m_Config == null || m_HttpClient == null)
            {
                throw new System.OperationCanceledException("Specter was not initialized correctly. Please call Specter.Initialize or set Specter Config to AutoInit");
            }
            
            var suffix = requestParams != null ? $"?{ToQueryString(requestParams)}" : "";
            var uri = $"{m_Config.BaseUrl}{endpoint}{suffix}";
            
            using var request = new HttpRequestMessage(method, uri);
            switch (authType)
            {
                case SPAuthType.AccessToken:
                    request.Headers.Authorization = new AuthenticationHeaderValue(SPApiAuthScheme.Bearer, m_Config.AccessToken);
                    break;
                case SPAuthType.None:
                    break;
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
                    var errResponse = new SPApiResponse<T>()
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
                
                var apiResponse = SpecterJson.DeserializeObject<SPApiResponse<T>>(resString);
                return apiResponse;
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                return null;
            }
        }

        public async Task<SPApiResponse<T>> GetAsync<T>(string endpoint, SPAuthType authType, object queryParams) where T: class
        {
            return await MakeRequestAsync<T>(HttpMethod.Get, endpoint, authType: authType, requestParams: queryParams);
        }

        public async Task<SPApiResponse<T>> PostAsync<T>(string endpoint, SPAuthType authType, object bodyParams) where T : class
        {
            return await MakeRequestAsync<T>(HttpMethod.Post, endpoint, authType: authType, requestBody: bodyParams);
        }

        public async Task<SPApiResponse<T>> PutAsync<T>(string endpoint, SPAuthType authType, object bodyParams) where T : class
        {
            return await MakeRequestAsync<T>(HttpMethod.Put, endpoint, authType: authType, requestBody: bodyParams);
        }
    }
}
