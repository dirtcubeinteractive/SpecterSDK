using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
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
        public List<SPPrizeDistributionRuleV2> Rules { get; set; }
        public string TimeOffsetSeconds { get; set; }

        public SPPrizeDistribution()
        {
            Rules = new List<SPPrizeDistributionRuleV2>();
            TimeOffsetSeconds = "0";
        }
        public SPPrizeDistribution(SPPrizeDistributionData data)
        {
            Rules = data.rules?.ConvertAll(x => new SPPrizeDistributionRuleV2(x)) ?? new List<SPPrizeDistributionRuleV2>();
        }
    }

    public class SPPrizeDistributionRuleV2 : ISpecterRewardable
    {
        public int SortOrder { get; set; }
        public int StartRank { get; set; }
        public int? EndRank { get; set; }
        
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        
        public SPPrizeDistributionRuleV2() { }
        public SPPrizeDistributionRuleV2(SPPrizeDistributionRuleData data)
        {
            SortOrder = data.no;
            StartRank = data.startRank;
            EndRank = data.endRank;
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
        }
    }
}