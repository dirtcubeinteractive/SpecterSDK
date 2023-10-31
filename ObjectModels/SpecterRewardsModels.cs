using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public abstract class SpecterRewardBase : SpecterResource, ISpecterMasterObject
    {
        public int Amount;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        protected SpecterRewardBase(SPRewardBaseData data)
        {
            
        }
    }

    public class SpecterCurrencyReward : SpecterRewardBase
    {
        public SpecterCurrencyReward(SPCurrencyRewardData data) : base(data)
        {
            
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
            ProgressionMarkers = new();
            Currencies = new();
            Items = new();
            Bundles = new();

            foreach (var progression in rewardDetails.progressionMarkers)
                ProgressionMarkers.Add(new SpecterProgressionMarkerReward(progression));

            foreach (var currency in rewardDetails.currencies)
                Currencies.Add(new SpecterCurrencyReward(currency));

            foreach (var items in rewardDetails.items)
                Items.Add(new SpecterItemReward(items));

            foreach (var bundle in rewardDetails.bundles)
                Bundles.Add(new SpecterBundleReward(bundle));

        }
    }
}