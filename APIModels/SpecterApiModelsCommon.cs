using System;
using System.Collections.Generic;
using SpecterSDK.API;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels
{
    [System.Serializable]
    public abstract class SPApiRequestBaseData { }

    [System.Serializable]
    public class SPApiRequestEntity
    {
        public string value { get; set; }
        public List<string> attributes { get; set; }
        public int limit { get; set; } = 1;
        public int? offset { get; set; } = 0;
    }

    [System.Serializable]
    public class SPApiResponse<T> where T: class, ISpecterApiResponseData, new()
    {
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<SPApiError> errors { get; set; }
        public T data { get; set; }
    }

    [Serializable]
    public class SPApiError
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
    
    [Serializable]
    public sealed class SPGeneralResponseDictionaryData : Dictionary<string, object>, ISpecterApiResponseData { }
    
    [Serializable]
    public sealed class SPResponseDataList<T> : List<T>, ISpecterApiResponseData where T : ISpecterApiResponseData { }

    public abstract class SpecterApiResultBase<T>
    where T: class, ISpecterApiResponseData, new()
    {
        public virtual bool LoadObjectsOnResponse => true;
        
        public SPApiResponse<T> Response { get; set; }
        public string Status => Response?.status;
        public int StatusCode => Response?.code ?? 500;
        public string Message => Response?.message;
        public List<SPApiError> Errors => Response?.errors;

        public bool HasError => Errors is { Count: > 0 } || Status is SPApiStatus.Error;

        public void InitSpecterObjects(bool force = false)
        {
            if (!force && !LoadObjectsOnResponse)
                return;
            InitSpecterObjectsInternal();
        }

        protected abstract void InitSpecterObjectsInternal();
    }

    public class SPGeneralResult : SpecterApiResultBase<SPGeneralResponseDictionaryData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
}