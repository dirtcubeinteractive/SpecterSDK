using System.Collections.Generic;
using System.Linq;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterReward : SpecterResource
    {
        public int Amount;
        public SPRewardType RewardType;
        public SpecterReward(SPRewardBaseData data) : base(data)
        {
            Amount = data.amount;
        }
        public SpecterReward(SPRewardBaseData data, SPRewardType rewardType) : base(data)
        {
            RewardType = rewardType;
            Amount = data.amount;
        }

    }

    public class SpecterRewardDetails
    {
        public List<SpecterReward> ProgressionMarkers;
        public List<SpecterReward> Currencies;
        public List<SpecterReward> Items;
        public List<SpecterReward> Bundles;

        public List<SpecterReward> AllRewards;
        public int RewardCount => AllRewards.Count;
        
        public SpecterRewardDetails()
        {
            AllRewards = new List<SpecterReward>();
            
            ProgressionMarkers = new List<SpecterReward>();

            Currencies = new List<SpecterReward>();

            Items = new List<SpecterReward>();

            Bundles = new List<SpecterReward>();
        }

        public SpecterRewardDetails(SPRewardDetailsResponseData rewardDetails)
        {
            AllRewards = new List<SpecterReward>();
            
            ProgressionMarkers = new List<SpecterReward>();
            if (rewardDetails.progressionMarkers != null)
            {
                foreach (var progression in rewardDetails.progressionMarkers)
                {
                    var reward = new SpecterReward(progression, SPRewardType.ProgressionMarker);
                    ProgressionMarkers.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Currencies = new List<SpecterReward>();
            if (rewardDetails.currencies != null)
            {
                foreach (var currency in rewardDetails.currencies)
                {
                    var reward = new SpecterReward(currency, SPRewardType.Currency);
                    Currencies.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Items = new List<SpecterReward>();
            if (rewardDetails.items != null)
            {
                foreach (var item in rewardDetails.items)
                {
                    var reward = new SpecterReward(item, SPRewardType.Item);
                    Items.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Bundles = new List<SpecterReward>();
            if (rewardDetails.bundles != null)
            {
                foreach (var bundle in rewardDetails.bundles)
                {
                    var reward = new SpecterReward(bundle, SPRewardType.Bundle);
                    Bundles.Add(reward);
                    AllRewards.Add(reward);
                }
            }
        }
    }

    public class SpecterRewardHistoryEntry : SpecterReward
    {
        public SPRewardClaimStatus Status;
        public SPRewardGrantType RewardGrant;
        public SPRewardSourceType SourceType;
        public string SourceId;
        public string SubSourceId;

        public SpecterRewardHistoryEntry(SPRewardHistoryEntryData data, SPRewardType rewardType) : base(data, rewardType)
        {
            Status = data.status;
            RewardGrant = data.rewardGrant;
            SourceType = data.sourceType;
            SourceId = data.sourceId;
        }
    }

    public class SpecterRewards
    {
        public SPRewardSourceType SourceType;
        public string SourceId;
        public string SubSourceId;
        public SPRewardClaimStatus Status;
        public SPRewardGrantType RewardGrant;

        public List<SpecterRewardHistoryEntry> Items;
        public List<SpecterRewardHistoryEntry> Bundles;
        public List<SpecterRewardHistoryEntry> Currencies;
        public List<SpecterRewardHistoryEntry> ProgressionMarkers;

        public SpecterRewards() { 
            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
        }

        public SpecterRewards(string sourceId, string subSourceId, SPRewardSourceType sourceType, SPRewardClaimStatus status, SPRewardGrantType rewardGrant)
        {
            SourceType = sourceType;
            SourceId = sourceId;
            SubSourceId = subSourceId;
            Status = status;
            RewardGrant = rewardGrant;

            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
        }
    }

}