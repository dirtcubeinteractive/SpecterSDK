using System.Collections.Generic;
using System.Linq;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterRewardResource : SpecterResource
    {
        public int Amount;
        public SPRewardType RewardType;
        
        protected SpecterRewardResource() { }
        public SpecterRewardResource(SPRewardResourceData data) : base(data)
        {
            Amount = data.amount;
        }
        public SpecterRewardResource(SPRewardResourceData data, SPRewardType rewardType) : base(data)
        {
            RewardType = rewardType;
            Amount = data.amount;
        }

    }

    public class SpecterRewardDetails
    {
        public List<SpecterRewardResource> ProgressionMarkers;
        public List<SpecterRewardResource> Currencies;
        public List<SpecterRewardResource> Items;
        public List<SpecterRewardResource> Bundles;

        public List<SpecterRewardResource> AllRewards;
        public int RewardCount => AllRewards.Count;
        
        public SpecterRewardDetails()
        {
            AllRewards = new List<SpecterRewardResource>();
            
            ProgressionMarkers = new List<SpecterRewardResource>();

            Currencies = new List<SpecterRewardResource>();

            Items = new List<SpecterRewardResource>();

            Bundles = new List<SpecterRewardResource>();
        }

        public SpecterRewardDetails(SPRewardResourceDetailsResponseData rewardDetails)
        {
            AllRewards = new List<SpecterRewardResource>();
            
            ProgressionMarkers = new List<SpecterRewardResource>();
            if (rewardDetails.progressionMarkers != null)
            {
                foreach (var progression in rewardDetails.progressionMarkers)
                {
                    var reward = new SpecterRewardResource(progression, SPRewardType.ProgressionMarker);
                    ProgressionMarkers.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Currencies = new List<SpecterRewardResource>();
            if (rewardDetails.currencies != null)
            {
                foreach (var currency in rewardDetails.currencies)
                {
                    var reward = new SpecterRewardResource(currency, SPRewardType.Currency);
                    Currencies.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Items = new List<SpecterRewardResource>();
            if (rewardDetails.items != null)
            {
                foreach (var item in rewardDetails.items)
                {
                    var reward = new SpecterRewardResource(item, SPRewardType.Item);
                    Items.Add(reward);
                    AllRewards.Add(reward);
                }
            }
            
            Bundles = new List<SpecterRewardResource>();
            if (rewardDetails.bundles != null)
            {
                foreach (var bundle in rewardDetails.bundles)
                {
                    var reward = new SpecterRewardResource(bundle, SPRewardType.Bundle);
                    Bundles.Add(reward);
                    AllRewards.Add(reward);
                }
            }
        }
    }

    public class SpecterRewardHistoryEntry : SpecterRewardResource
    {
        /// <summary>
        /// Status of the reward. See <see cref="SPRewardClaimStatus"/>
        /// </summary>
        public SPRewardClaimStatus Status;
        
        /// <summary>
        /// Grant mechanism for the reward - server or client side grant. See <see cref="SPRewardGrantType"/>>
        /// </summary>
        public SPRewardGrantType RewardGrant;
        
        /// <summary>
        /// The type of the source for the reward (eg: task, level, competition, etc. See <see cref="SPRewardSourceType"/>
        /// </summary>
        public SPRewardSourceType SourceType;
        
        /// <summary>
        /// The ID of the reward source.
        /// </summary>
        public string SourceId;
        
        /// <summary>
        /// ID of the instance of the source for which this reward is/should be granted (eg: recurring tasks)
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// Custom meta data provided by you when granting the reward.
        /// </summary>
        public Dictionary<string, object> Meta;

        public SpecterRewardHistoryEntry() { }
        public SpecterRewardHistoryEntry(SPRewardHistoryEntryData data, SPRewardType rewardType) : base(data, rewardType)
        {
            Status = data.status;
            RewardGrant = data.rewardGrant;
            SourceType = data.sourceType;
            SourceId = data.sourceId;
            InstanceId = data.instanceId;
            Meta = data.meta ?? new Dictionary<string, object>();
        }

        public bool TryGetMeta<T>(string key, out T val)
        {
            if (Meta.TryGetValue(key, out var obj))
            {
                if (obj is T convertedObj)
                {
                    val = convertedObj;
                    return true;
                }
            }

            val = default;
            return false;
        }
    }

    public class SpecterRewardSet
    {
        public SPRewardSourceType SourceType;
        public string SourceId;
        public string InstanceId;
        public SPRewardClaimStatus Status;
        public SPRewardGrantType RewardGrant;

        public List<SpecterRewardHistoryEntry> Items;
        public List<SpecterRewardHistoryEntry> Bundles;
        public List<SpecterRewardHistoryEntry> Currencies;
        public List<SpecterRewardHistoryEntry> ProgressionMarkers;

        public List<SpecterRewardHistoryEntry> PendingRewards;
        public List<SpecterRewardHistoryEntry> CompletedRewards;

        public SpecterRewardSet() { 
            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
            PendingRewards = new List<SpecterRewardHistoryEntry>();
            CompletedRewards = new List<SpecterRewardHistoryEntry>();
        }

        public SpecterRewardSet(SpecterRewardHistoryEntry entry)
        {
            SourceType = entry.SourceType;
            SourceId = entry.SourceId;
            Status = entry.Status;
            RewardGrant = entry.RewardGrant;
            InstanceId = entry.InstanceId;
            
            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
            PendingRewards = new List<SpecterRewardHistoryEntry>();
            CompletedRewards = new List<SpecterRewardHistoryEntry>();
        }

        public void AddReward(SpecterRewardHistoryEntry entry)
        {
            switch (entry.RewardType)
            {
                case SPRewardType.ProgressionMarker:
                    ProgressionMarkers.Add(entry);
                    break;
                case SPRewardType.Bundle:
                    Bundles.Add(entry);
                    break;
                case SPRewardType.Item:
                    Items.Add(entry);
                    break;
                case SPRewardType.Currency:
                    Currencies.Add(entry);
                    break;
            }
            
            if (entry.Status == SPRewardClaimStatus.Pending)
            {
                PendingRewards.Add(entry);
                Status = SPRewardClaimStatus.Pending; 
            }

            if (entry.Status == SPRewardClaimStatus.Completed)
            {
                CompletedRewards.Add(entry);
            }
        }
    }

    public class SpecterPrizeDistribution
    {
        public int StartRank;
        public int? EndRank;
        public SpecterRewardDetails Rewards;
        
        public SpecterPrizeDistribution() { }

        public SpecterPrizeDistribution(SPPrizeDistributionData data)
        {
            StartRank = data.startRank;
            EndRank = data.endRank;
            Rewards = new SpecterRewardDetails(data.rewardDetails);
        }
    }

}