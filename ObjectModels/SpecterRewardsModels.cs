using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public abstract class SpecterRewardBase : SpecterResource, ISpecterMasterObject
    {
        public int Amount;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        protected SpecterRewardBase(SPRewardBaseData data)
        {
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Meta = data.meta;
            Tags = data.tags;
            Amount = data.amount;
        }
    }

    public class SpecterCurrencyReward : SpecterRewardBase
    {
        public string Code;
        public SPCurrencyType Type;

        public SpecterCurrencyReward(SPCurrencyRewardData data) : base(data)
        {
            Code = data.code;
            Type = data.type;
        }
    }

    public abstract class SpecterItemRewardBase : SpecterRewardBase
    {
        protected SpecterItemRewardBase(SPItemRewardBaseData data) : base(data)
        {

        }
    }

    public class SpecterItemReward : SpecterItemRewardBase
    {
        public SpecterItemReward(SPItemRewardData data) : base(data)
        {

        }
    }

    public class SpecterProgressionMarkerReward : SpecterRewardBase
    {
        public SpecterProgressionMarkerReward(SPProgressionMarkerRewardData data) : base(data)
        {

        }
    }

    public class SpecterBundleReward : SpecterItemRewardBase
    {
        public SpecterBundleReward(SPBundleRewardData data) : base(data)
        {

        }
    }

    public class SpecterReward
    {
        public List<SpecterProgressionMarkerReward> ProgressionMarkers;
        public List<SpecterCurrencyReward> Currencies;
        public List<SpecterItemReward> Items;
        public List<SpecterBundleReward> Bundles;

        public SpecterReward(SPRewardDetailsResponseData rewardDetails)
        {
            ProgressionMarkers = new List<SpecterProgressionMarkerReward>();
            Currencies = new List<SpecterCurrencyReward>();
            Items = new List<SpecterItemReward>();
            Bundles = new List<SpecterBundleReward>();

            if (rewardDetails.progressionMarkers != null)
            {
                foreach (var progression in rewardDetails.progressionMarkers)
                    ProgressionMarkers.Add(new SpecterProgressionMarkerReward(progression));
            }
            if (rewardDetails.currencies != null)
            {
                foreach (var currency in rewardDetails.currencies)
                    Currencies.Add(new SpecterCurrencyReward(currency));
            }
            if (rewardDetails.items != null)
            {
                foreach (var items in rewardDetails.items)
                    Items.Add(new SpecterItemReward(items));
            }
            if (rewardDetails.bundles != null)
            {
                foreach (var bundle in rewardDetails.bundles)
                    Bundles.Add(new SpecterBundleReward(bundle));
            }
        }
    }

    public class SpecterRewardHistoryEntry : SpecterRewardBase
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

    public class SpecterCurrencyRewardHistoryEntry : SpecterRewardHistoryEntry
    {
        public string Code;
        public SPCurrencyType Type;
        public SpecterCurrencyRewardHistoryEntry(SPCurrencyRewardHistoryEntryData data) : base(data)
        {
            Code = data.code;
            Type = data.type;
        }
    }
}