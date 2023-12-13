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
    public class SPGetProgressionSystemsRequest : SPApiRequestBase
    {
        public List<string> progressionSystemIds { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    [Serializable]
    public class SPGetProgressionSystemsResult : SpecterApiResultBase<SPProgressionSystemDataList>
    {
        public List<SpecterProgressionSystem> ProgressionSystems;
        protected override void InitSpecterObjectsInternal()
        {
            ProgressionSystems = new();
            foreach (var progressionSystem in Response.data)
            {
                ProgressionSystems.Add(new SpecterProgressionSystem(progressionSystem));
            }
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetProgressionSystemsResult> GetProgressionSystemMasterAsync(SPGetProgressionSystemsRequest request)
        {
            var result = await PostAsync<SPGetProgressionSystemsResult, SPProgressionSystemDataList>("/v1/client/app/get-progression-system", AuthType, request);
            return result;
        }
    }
}

