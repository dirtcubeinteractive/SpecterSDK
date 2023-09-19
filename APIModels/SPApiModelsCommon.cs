using System.Collections.Generic;

namespace SpecterSDK.APIModels
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
    public class SPModel { }

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
}