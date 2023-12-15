using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using UnityEngine.Serialization;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchesRequest : SPApiRequestBase
    {
        public List<string> gameIds { get; set; }
        public List<string> matchIds { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<string> attributes { get; set; }
    }
    
    public class SPGetMatchesResult : SpecterApiResultBase<SPGetMatchesResponseData>
    {
        public List<SpecterMatch> Matches; 
        public int TotalMatchCount;

        protected override void InitSpecterObjectsInternal()
        {
            Matches = new List<SpecterMatch>();
            foreach (var match in Response.data.matches)
            {
                Matches.Add(new SpecterMatch(match));
            }
            TotalMatchCount = Response.data.totalCount;
        }
    }
    
    public partial class SPAppApiClient
    {
        public async Task<SPGetMatchesResult> GetMatchesAsync(SPGetMatchesRequest request)
        {
            var result = await PostAsync<SPGetMatchesResult, SPGetMatchesResponseData>("/v1/client/app/get-matches",AuthType,request);
            return result;
        }
    }
}