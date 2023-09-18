using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SpecterSDK.Models;

namespace SpecterSDK.APIClients
{
    [System.Serializable]
    public class SPUserGetProfileRequest : SPApiRequestBase
    {
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    [System.Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUserUpdateProfileRequest : SPApiRequestBase
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string birthdate { get; set; }
        public string customId { get; set; }
        public bool? isKyc { get; set; }
    }

    public class SPUserApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPUserApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPApiResponse<SPUserProfile>> GetProfile(SPUserGetProfileRequest request)
        {
            List<string> defaultAttributes = new List<string>()
            {
                "id",
                "username",
                "customId",
            };

            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();

            var response = await PostAsync<SPUserProfile>("/v1/client/user/profile", AuthType, request);
            return response;
        }

        public async Task<SPApiResponse<object>> UpdateProfile(SPUserUpdateProfileRequest request)
        {
            var response = await PutAsync<object>("/v1/client/user/update", AuthType, request);
            return response;
        }
    }
}