using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPItemReward : ISpecterRewardedResource, ISpecterEconomyResource 
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        
        public SPResourceType ResourceType => SPResourceType.Item;
        public long Amount { get; set; }
        
        public SPItemReward() { }
        public SPItemReward(SPRewardedItemData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Amount = data.amount;
            
            Rarity = (SPRarity)data.rarity.id;
        }
    }

    public class SPBundleReward : ISpecterRewardedResource, ISpecterEconomyResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        
        public SPResourceType ResourceType => SPResourceType.Bundle;
        public long Amount { get; set; }
        
        public SPBundleReward() { }
        public SPBundleReward(SPRewardedBundleData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Amount = data.amount;

            Rarity = (SPRarity)data.rarity.id;
        }
    }

    public class SPCurrencyReward : ISpecterRewardedResource, ISpecterCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        
        public SPResourceType ResourceType => SPResourceType.Currency;
        public long Amount { get; set; }
        
        public string Code { get; set; }
        public SPCurrencyType Type { get; set; }
        
        public bool IsVirtual => Type == SPCurrencyType.Virtual;
        public bool IsReal => Type == SPCurrencyType.Real;
        
        public SPCurrencyReward() { }
        public SPCurrencyReward(SPRewardedCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Amount = data.amount;
            
            Rarity = (SPRarity)data.rarity.id;
            
            Code = data.code;
            Type = data.type;
        }
    }

    public class SPProgressionMarkerReward : ISpecterRewardedResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPResourceType ResourceType => SPResourceType.ProgressionMarker;
        public long Amount { get; set; }
        
        public SPProgressionMarkerReward() { }
        public SPProgressionMarkerReward(SPRewardedProgressionMarkerData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Amount = data.amount;
        }
    }

    public class SPRewards
    {
        private readonly List<SPItemReward> ItemRewards = new List<SPItemReward>();
        private readonly List<SPBundleReward> BundleRewards = new List<SPBundleReward>();
        private readonly List<SPCurrencyReward> CurrencyRewards = new List<SPCurrencyReward>();
        private readonly List<SPProgressionMarkerReward> ProgressionMarkerRewards = new List<SPProgressionMarkerReward>();
        
        public IReadOnlyList<SPItemReward> Items => ItemRewards;
        public IReadOnlyList<SPBundleReward> Bundles => BundleRewards;
        public IReadOnlyList<SPCurrencyReward> Currencies => CurrencyRewards;
        public IReadOnlyList<SPProgressionMarkerReward> ProgressionMarkers => ProgressionMarkerRewards;
        
        private readonly List<ISpecterRewardedResource> AllRewards = new List<ISpecterRewardedResource>();
        public IReadOnlyList<ISpecterRewardedResource> All => AllRewards;

        public SPRewards() { }
        public SPRewards(SPRewardsData data)
        {
            ItemRewards = data.items?.ConvertAll(x =>
            {
                var itemReward = new SPItemReward(x);
                AllRewards.Add(itemReward);
                return itemReward;
            }) ?? new List<SPItemReward>();
            
            BundleRewards = data.bundles?.ConvertAll(x =>  
            {
                var bundleReward = new SPBundleReward(x);
                AllRewards.Add(bundleReward);
                return bundleReward;
            }) ?? new List<SPBundleReward>();

            CurrencyRewards = data.currencies?.ConvertAll(x =>
            {
                var currencyReward = new SPCurrencyReward(x);
                AllRewards.Add(currencyReward);
                return currencyReward;
            }) ?? new List<SPCurrencyReward>();

            ProgressionMarkerRewards = data.progressionMarkers?.ConvertAll(x =>
            {
                var progressionMarkerReward = new SPProgressionMarkerReward(x);
                AllRewards.Add(progressionMarkerReward);
                return progressionMarkerReward;
            }) ?? new List<SPProgressionMarkerReward>();
        }
        
        /// <summary>
        /// Add a reward to the relevant list in the rewards object.
        /// </summary>
        /// <param name="reward">Reward to add</param>
        /// <exception cref="NotImplementedException">Exception thrown if the reward is of an unimplemented resource type</exception>
        public void AddReward(ISpecterRewardedResource reward)
        {
            switch (reward.ResourceType)
            {
                case SPResourceType.Item:
                    ItemRewards.Add((SPItemReward)reward);
                    break;
                case SPResourceType.Bundle:
                    BundleRewards.Add((SPBundleReward)reward);
                    break;
                case SPResourceType.Currency:
                    CurrencyRewards.Add((SPCurrencyReward)reward);
                    break;
                case SPResourceType.ProgressionMarker:
                    ProgressionMarkerRewards.Add((SPProgressionMarkerReward)reward);
                    break;
                default:
                    throw new NotImplementedException("Cannot add unknown reward type " + reward.ResourceType);
            }
            
            AllRewards.Add(reward);
        }

        public bool RemoveReward(ISpecterRewardedResource reward)
        {
            var element = AllRewards.Find(r => r.Id == reward.Id);
            if (element == null)
                return false;
            
            return reward.ResourceType switch
            {
                SPResourceType.Item => ItemRewards.Remove((SPItemReward)element),
                SPResourceType.Bundle => BundleRewards.Remove((SPBundleReward)element),
                SPResourceType.Currency => CurrencyRewards.Remove((SPCurrencyReward)element),
                SPResourceType.ProgressionMarker => ProgressionMarkerRewards.Remove((SPProgressionMarkerReward)element),
                _ => throw new NotImplementedException("Cannot remove unknown reward type " + reward.ResourceType)
            } && AllRewards.Remove(element);
        }

        /// <summary>
        /// Overwrite and set rewards in this rewards object.
        /// </summary>
        /// <param name="rewards"></param>
        public void SetRewards(List<ISpecterRewardedResource> rewards)
        {
            ItemRewards.Clear();
            BundleRewards.Clear();
            CurrencyRewards.Clear();
            ProgressionMarkerRewards.Clear();
            AllRewards.Clear();
            
            foreach (var reward in rewards)
            {
                AddReward(reward);
            }
        }
    }

    public class SPPrizeDistribution
    {
        public List<SPPrizeDistributionRule> Rules { get; set; }
        public string TimeOffsetSeconds { get; set; }

        public SPPrizeDistribution()
        {
            Rules = new List<SPPrizeDistributionRule>();
            TimeOffsetSeconds = "0";
        }
        public SPPrizeDistribution(SPPrizeDistributionData data, SPRewardSourceType sourceType)
        {
            Rules = data.rules?.ConvertAll(x => new SPPrizeDistributionRule(x, sourceType)) ?? new List<SPPrizeDistributionRule>();
        }
    }

    public class SPPrizeDistributionRule : ISpecterRewardable
    {
        public int SortOrder { get; set; }
        public int StartRank { get; set; }
        public int? EndRank { get; set; }
        
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        
        public SPRewardSourceType RewardSource { get; set; }
        
        public SPPrizeDistributionRule() { }
        public SPPrizeDistributionRule(SPPrizeDistributionRuleData data, SPRewardSourceType rewardSource)
        {
            SortOrder = data.no;
            StartRank = data.startRank;
            EndRank = data.endRank;
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
            RewardSource = rewardSource;
        }
    }

    public class SPRewardHistoryEntry
    {
        public string InstanceId { get; set; }
        public SPRewardClaimStatus Status { get; set; }
        public SPRewardSourceType SourceType { get; set; }
        public string SourceId { get; set; }
        public SPRewards RewardDetails { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPRewardHistoryEntry() { }
        public SPRewardHistoryEntry(SPRewardHistoryEntryDataV2 data)
        {
            InstanceId = data.instanceId;
            Status = data.status;
            SourceType = data.sourceType;
            SourceId = data.sourceId;
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
            Meta = data.meta;
        }

        public bool TryGetMeta<T>(string key, out T val)
        {
            bool success = false;
            val = default;
            
            try
            {
                if (Meta == null)
                    throw new NullReferenceException("Meta is null");
                
                if (Meta.TryGetValue(key, out object objVal))
                {
                    if (SpecterJson.TryConvertObject<T>(objVal, out val))
                        success = true;
                }
                else
                    SPDebug.LogWarning($"No meta data with key {key} exists. Please check your configuration on the Specter dashboard");
            }
            catch (Exception e)
            {
                SPDebug.LogError($"Failed to deserialize meta data {key} with {e.GetType().Name}: {e.Message}");
            }

            return success;
        }
    }

    public class SPFailedRewardSource
    {
        public string Id { get; set; }
        public string InstanceId { get; set; }
        public SPRewardSourceType Type { get; set; }
        
        public SPFailedRewardSource() { }
        public SPFailedRewardSource(SPFailedRewardSourceData data)
        {
            Id = data.id;
            InstanceId = data.instanceId;
            Type = data.type;
        }
    }

    public class SPFailedResourceInfo
    {
        public string Id { get; set; }
        public long Amount { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        
        public SPResourceType ResourceType { get; set; }
        public int Code { get; set; }
        
        public SPFailedResourceInfo() { }

        public SPFailedResourceInfo(SPFailedResourceInfoData data, SPResourceType resourceType)
        {
            Id = data.id;
            Amount = data.amount;
            Message = data.message;
            Reason = data.reason;
            ResourceType = resourceType;
            Code = data.code;
        }
    }

    public class SPFailedInventoryEntityInfo : SPFailedResourceInfo
    {
        public string StackId { get; set; }
        public string CollectionId { get; set; }
        
        public SPFailedInventoryEntityInfo() : base() {}
        public SPFailedInventoryEntityInfo(SPFailedInventoryEntityData data, SPResourceType resourceType) : base(data, resourceType)
        {
            StackId = data.stackId;
            CollectionId = data.collectionId;
        }
    }

    public class SPFailedRewards
    {
        public SPFailedRewardSource Source { get; set; }
        
        public List<SPFailedInventoryEntityInfo> ItemsFailed { get; set; }
        public List<SPFailedInventoryEntityInfo> BundlesFailed { get; set; }
        public List<SPFailedResourceInfo> CurrenciesFailed { get; set; }
        public List<SPFailedResourceInfo> ProgressionMarkersFailed { get; set; }
        
        public SPFailedRewards() { }
        public SPFailedRewards(SPFailedRewardsData data)
        {
            Source = new SPFailedRewardSource(data.source);

            ItemsFailed = data.itemsFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Item)) ?? new List<SPFailedInventoryEntityInfo>();
            BundlesFailed = data.bundlesFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Bundle)) ?? new List<SPFailedInventoryEntityInfo>();
            CurrenciesFailed = data.currenciesFailed?.ConvertAll(x => new SPFailedResourceInfo(x, SPResourceType.Currency)) ?? new List<SPFailedResourceInfo>();
            ProgressionMarkersFailed = data.progressionMarkersFailed?.ConvertAll(x => new SPFailedResourceInfo(x, SPResourceType.ProgressionMarker)) ?? new List<SPFailedResourceInfo>();
        }
    }
}