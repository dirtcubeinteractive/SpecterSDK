using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Achievements
{
    [Serializable]
    public class SPGrantRewardBySourceResponse : ISpecterApiResponseData
    {
        public List<SPInventoryItemData> items { get; set; }
        public List<SPInventoryBundleData> bundles { get; set; }
        public List<SPWalletCurrencyData> currencies { get; set; }
        public List<SPMarkerProgressData> progressionMarkers { get; set; }
        
        public List<SPFailedRewardsData> failedRewards { get; set; }
    }
}