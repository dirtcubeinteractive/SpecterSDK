using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Inventory
{
    public class SPRemoveFromInventoryResponse : ISpecterApiResponseData
    {
        public List<SPFailedInventoryEntityData> itemsFailed { get; set; }
        public List<SPFailedInventoryEntityData> bundlesFailed { get; set; }
    }
}