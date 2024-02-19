using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetAppInfoRequest : SPApiRequestBase
    {
        public List<string> attributes { get; set; }
    }

    public class SPGetAppInfoResult
    {
        
    }
    
    public partial class SPAppApiClient
    {
        
    }
}