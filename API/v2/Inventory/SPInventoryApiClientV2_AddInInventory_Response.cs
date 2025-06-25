using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Inventory
{
    public class SPAddInInventoryResponse : ISpecterApiResponseData
    {
        public List<SPFailedInventoryEntityData> itemsFailed { get; set; }
        public List<SPFailedInventoryEntityData> bundlesFailed { get; set; }
    }
}