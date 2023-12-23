using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Progression
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProgressRequest : SPPaginatedApiRequest
    {
        public List<string> progressionMarkerIds { get; set; }
        public string sortOrder { get; set; }
        public string sortField { get; set; }
    }

    public class SPGetUserProgressResult : SpecterApiResultBase<SPUserProgressDataList>
    {
        public List<SpecterUserProgress> Progressions;
        protected override void InitSpecterObjectsInternal()
        {
            Progressions = new List<SpecterUserProgress>();
            foreach (var progressionData in Response.data)
            {
                Progressions.Add(new SpecterUserProgress(progressionData));  
            }
        }
    }

    public partial class SPProgressionApiClient
    {
        public async Task<SPGetUserProgressResult> GetUserProgressAsync(SPGetUserProgressRequest request)
        {
            var result = await PostAsync<SPGetUserProgressResult, SPUserProgressDataList>("/v1/client/progression/get-progress", AuthType, request);
            return result;
        }

    }
}