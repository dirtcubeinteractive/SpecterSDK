using System;
using System.Collections.Generic;
using SpecterSDK.APIDataModels.Interfaces;

namespace SpecterSDK.APIDataModels
{
    [System.Serializable]
    public abstract class SPApiRequestBase { }

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
    public class SPGeneralResponseDictionaryData : Dictionary<string, object>, ISpecterApiResponseData { }

    [Serializable]
    public class SPApiError
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }

    public abstract class SPApiResultBase<TSelf, TData> 
        where TData: class, ISpecterApiResponseData, new()
        where TSelf: SPApiResultBase<TSelf, TData>, new()
    {
        public SPApiResponse<TData> ResponseRaw { get; set; }
        public TData DataRaw => ResponseRaw?.data;
        public bool IsError => ResponseRaw?.errors is { Count: > 0 };

        public static TSelf Create(SPApiResponse<TData> response)
        {
            var self = new TSelf
            {
                ResponseRaw = response
            };
            
            self.CreateInternal();
            return self;
        }

        protected abstract void CreateInternal();
    }
}