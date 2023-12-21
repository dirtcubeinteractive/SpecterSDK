using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterReward : SpecterResource
    {
        public int Amount;
        
        public SpecterReward(SPRewardBaseData data) : base(data)
        {
            Amount = data.amount;
        }
    }

    public class SpecterRewards
    {
        public List<SpecterReward> ProgressionMarkers;
        public List<SpecterReward> Currencies;
        public List<SpecterReward> Items;
        public List<SpecterReward> Bundles;

        public SpecterRewards(SPRewardDetailsResponseData rewardDetails)
        {
            ProgressionMarkers = new List<SpecterReward>();
            if (rewardDetails.progressionMarkers != null)
            {
                foreach (var progression in rewardDetails.progressionMarkers)
                    ProgressionMarkers.Add(new SpecterReward(progression));
            }
            
            Currencies = new List<SpecterReward>();
            if (rewardDetails.currencies != null)
            {
                foreach (var currency in rewardDetails.currencies)
                    Currencies.Add(new SpecterReward(currency));
            }
            
            Items = new List<SpecterReward>();
            if (rewardDetails.items != null)
            {
                foreach (var items in rewardDetails.items)
                    Items.Add(new SpecterReward(items));
            }
            
            Bundles = new List<SpecterReward>();
            if (rewardDetails.bundles != null)
            {
                foreach (var bundle in rewardDetails.bundles)
                    Bundles.Add(new SpecterReward(bundle));
            }
        }
    }

    public class SpecterRewardHistoryEntry : SpecterReward
    {
        public SPRewardClaimStatus Status;
        public SPRewardGrantType RewardGrant;
        public SPRewardSourceType SourceType;
        public string SourceId;

        public SpecterRewardHistoryEntry(SPRewardHistoryEntryData data) : base(data)
        {
            Status = data.status;
            RewardGrant = data.rewardGrant;
            SourceType = data.sourceType;
            SourceId = data.sourceId;
        }
    }
}