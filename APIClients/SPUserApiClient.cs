using System.Collections.Generic;
using System.Linq;
using SpecterSDK.Models;

namespace SpecterSDK.APIClients
{
    public class SPUserGetProfileRequest
    {
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPUserApi: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPUserApi(SpecterRuntimeConfig config) : base(config) {}

        public async void GetProfile(List<string> userAttributes, List<SPApiRequestEntity> userEntities)
        {
            string endpoint = "/v1/client/user/profile";

            List<string> attributes = new List<string>()
            {
                "id",
                "firstName",
                "lastName",
                "username",
                "customId"
            };
            
            attributes.AddRange(userAttributes ?? new List<string>());

            Dictionary<string, SPApiRequestEntity> entityMap = new Dictionary<string, SPApiRequestEntity>();

            if (userEntities != null)
            {
                foreach (var entity in userEntities)
                {
                    entityMap[entity.value] = entity;
                }
            }

            SPUserGetProfileRequest requestBody = new SPUserGetProfileRequest()
            {
                attributes = attributes.Distinct().ToList(),
                entities = entityMap.Values.ToList()
            };

            var response = await PostAsync<SPUserProfileResponseData>(endpoint, AuthType, requestBody);
        }
    }
}