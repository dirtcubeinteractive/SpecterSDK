using System.Collections.Generic;
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

        public SpecterRewardDetails()
        {
            ProgressionMarkers = new List<SpecterReward>();

            Currencies = new List<SpecterReward>();

            Items = new List<SpecterReward>();

            Bundles = new List<SpecterReward>();
        }

        public SpecterRewardDetails(SPRewardDetailsResponseData rewardDetails)
        {
            ProgressionMarkers = new List<SpecterReward>();
            if (rewardDetails.progressionMarkers != null)
            {
                foreach (var progression in rewardDetails.progressionMarkers)
                    ProgressionMarkers.Add(new SpecterReward(progression,SPRewardType.ProgressionMarker));
            }
            
            Currencies = new List<SpecterReward>();
            if (rewardDetails.currencies != null)
            {
                foreach (var currency in rewardDetails.currencies)
                    Currencies.Add(new SpecterReward(currency,SPRewardType.Currency));
            }
            
            Items = new List<SpecterReward>();
            if (rewardDetails.items != null)
            {
                foreach (var items in rewardDetails.items)
                    Items.Add(new SpecterReward(items,SPRewardType.Item));
            }
            
            Bundles = new List<SpecterReward>();
            if (rewardDetails.bundles != null)
            {
                foreach (var bundle in rewardDetails.bundles)
                    Bundles.Add(new SpecterReward(bundle,SPRewardType.Bundle));
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

        public SpecterRewards(string sourceId, SPRewardSourceType sourceType)
        {
            SourceId = sourceId;
            SourceType = sourceType;

            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
        }

        public SpecterRewards(string sourceId, SPRewardSourceType sourceType, SPRewardClaimStatus status, SPRewardGrantType rewardGrant)
        {
            SourceType = sourceType;
            SourceId = sourceId;
            Status = status;
            RewardGrant = rewardGrant;

            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
        }
    }

}