using System;
using Newtonsoft.Json;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    public partial class SPInventoryApiClient : SpecterApiClientBase
    {
        [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class SPInventoryEntityInfo
        {
            public string id;
            public int? amount;
            public string collectionId;
        }
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        public SPInventoryApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}