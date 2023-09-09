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

namespace SpecterSDK
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

    public class SpecterApiClientBase
    {
        protected readonly HttpClient m_HttpClient;
        protected readonly SpecterRuntimeConfig m_Config;

        public SpecterApiClientBase(SpecterConfigData configData)
        {
            m_Config = new SpecterRuntimeConfig(configData);
            m_HttpClient = new HttpClient();
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
            object requestParams = null, 
            object requestBody = null
            ) where T: class
        {
            var suffix = requestParams != null ? $"?{ToQueryString(requestParams)}" : "";
            var uri = $"{m_Config.BaseUrl}{endpoint}{suffix}";
            
            using var request = new HttpRequestMessage(method, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue(SPApiAuthScheme.Bearer, m_Config.AccessToken);

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

        public async Task<SPApiResponse<T>> GetAsync<T>(string endpoint, object queryParams) where T: class
        {
            return await MakeRequestAsync<T>(HttpMethod.Get, endpoint, requestParams: queryParams);
        }

        public async Task<SPApiResponse<T>> PostAsync<T>(string endpoint, object bodyParams) where T : class
        {
            return await MakeRequestAsync<T>(HttpMethod.Post, endpoint, requestBody: bodyParams);
        }

        public async Task<SPApiResponse<T>> PutAsync<T>(string endpoint, object bodyParams) where T : class
        {
            return await MakeRequestAsync<T>(HttpMethod.Put, endpoint, requestBody: bodyParams);
        }
    }
}
