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
    public class SPGetGamesRequest : SPPaginatedApiRequest
    {
       public List<string> gameIds { get; set; }
       public List<string> attributes { get; set; }
       public string search { get; set; }
       public List<SPApiRequestEntity> entities { get; set; }
    }
    
    public class SPGetGamesResult : SpecterApiResultBase<SPGetGamesResponseData>
    {
        public List<SpecterGame> Games;
        public int TotalGameCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            Games = new List<SpecterGame>();
            foreach (var game in Response.data.games)
            {
                Games.Add(new SpecterGame(game));
            }
            TotalGameCount = Response.data.totalCount;
        }   
    }
        
    public partial class SPAppApiClient
    {
        public async Task<SPGetGamesResult> GetGamesAsync(SPGetGamesRequest request)
        {
            var result = await PostAsync<SPGetGamesResult, SPGetGamesResponseData>("/v1/client/app/get-games",AuthType,request);
            return result;
        }
    }
}