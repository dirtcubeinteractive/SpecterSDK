using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Progression
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateProgressionMarkerRequest : SPApiRequestBaseData
    {   
        public string operation { get; set; }
        public int amount { get; set; }
        public string progressionMarkerId { get; set; }
    }

    public class SPUpdateProgressionMarkerResult : SpecterApiResultBase<SPUserProgressResponseData>
    {
        public SpecterUserProgress UpdatedProgression;
        protected override void InitSpecterObjectsInternal()
        {
            UpdatedProgression = new SpecterUserProgress(Response.data);
        }
    }

    public partial class SPProgressionApiClient
    {
        public async Task<SPUpdateProgressionMarkerResult> UpdateProgressionMarkerAsync(SPUpdateProgressionMarkerRequest request)
        {
            var task = await PostAsync<SPUpdateProgressionMarkerResult, SPUserProgressResponseData>("/v1/client/progression/update-marker", AuthType, request);
            return task;
        }
    }
}