using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetGamesRequest : SPApiRequestBaseData
    {
       public List<string> gameIds { get; set; }
       public List<string> attributes { get; set; }
       public int offset { get; set; }
       public int limit { get; set; }
       public string search { get; set; }
       public List<SPApiRequestEntity> entities { get; set; }
    }

    [Serializable]
    public class SPGetGamesResult : SpecterApiResultBase<SPGameResponseDataList>
    {
        public List<SpecterGame> Games;
        protected override void InitSpecterObjectsInternal()
        {
            Games = new List<SpecterGame>();
            foreach (var game in Response.data)
            {
                Games.Add(new SpecterGame(game));
            }
        }   
    }
        
    public partial class SPAppApiClient
    {
        public async Task<SPGetGamesResult> GetGamesAsync(SPGetGamesRequest request)
        {
            var result = await PostAsync<SPGetGamesResult,SPGameResponseDataList>("/v1/client/app/get-games",AuthType,request);
            return result;
        }
    }
}