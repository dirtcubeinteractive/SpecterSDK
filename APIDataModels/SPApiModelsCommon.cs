using System;
using System.Collections.Generic;
using SpecterSDK.APIDataModels.Interfaces;

namespace SpecterSDK.APIDataModels
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
    
    public abstract class SPApiResultBase<TSelf, TData> 
        where TData: class, ISpecterApiResponseData, new()
        where TSelf: SPApiResultBase<TSelf, TData>, new()
    {
        protected const string CONSTRUCTOR_USAGE_WARN = "Constructor is not meant to be used. Use Create function & override the LoadFromData instead.";
        
        [Obsolete(CONSTRUCTOR_USAGE_WARN, false)]
        protected SPApiResultBase() { }
        
        public SPApiResponse<TData> ResponseRaw { get; set; }
        public bool IsError => ResponseRaw?.errors is { Count: > 0 };

        /*
         * PLEASE DISCUSS WITH TEAM IF THIS SHOULD BE KEPT FOR USE
        public static TSelf Create()
        {
            var self = new TSelf();
            return self;
        }
        *
        */
        
        public static TSelf Create(SPApiResponse<TData> response)
        {
            var self = new TSelf() { ResponseRaw = response};
            if (response.data != null)
                self.LoadFromData(response.data);
            return self;
        }

        protected virtual void CreateInternal() { }
        protected abstract void LoadFromData(TData data);
    }

    public class SPGeneralResult : SPApiResultBase<SPGeneralResult, SPGeneralResponseDictionaryData>
    {
        public Dictionary<string, object> ResultDict;
        
        protected override void LoadFromData(SPGeneralResponseDictionaryData data)
        {
            ResultDict = data;
        }
    }
}