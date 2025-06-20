using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyRewardHistoryResponse : List<SPRewardHistoryEntryDataV2>, ISpecterApiResponseData { }

    [Serializable]
    public class SPRewardHistoryEntryDataV2
    {
        public string instanceId { get; set; }
        public SPRewardClaimStatus status { get; set; }
        public SPRewardSourceType sourceType { get; set; }
        public string sourceId { get; set; }
        public SPRewardsData rewardDetails { get; set; }
    }
}