using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardsRequest : SPApiRequestBaseData
    {
        public List<SPRewardGrantDetail> rewardDetails;
    }

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardGrantDetail
    {
        public SPRewardSource source;
        public SPRewards rewards;
    }

    public class SPRewardSource
    {
        public string id { get; set; }
        public SPRewardSourceType type { get; set; }
    }
    public abstract class SPGeneralRewardRequestBase
    {
        public string id;
        public int amount;
    }
    public class SPItem : SPGeneralRewardRequestBase
    {
        public string collectionId;
        public SPItem(string id,int amount ,string collectionId)
        {
            this.id = id;
            this.amount = amount;
            this.collectionId = collectionId;
        }
    }
    public class SPBundle : SPItem
    {
        public SPBundle(string id, int amount, string collectionId) : base(id,amount,collectionId)
        {}
    }
    public class SPProgressionMarker : SPGeneralRewardRequestBase
    {
        public SPProgressionMarker(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
    public class SPCurrency : SPGeneralRewardRequestBase
    {
        public SPCurrency(string id, int amount)
        {
           this.id = id;
           this.amount = amount;
        }
    }
    
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewards
    {
        public List<SPItem> items;
        public List<SPProgressionMarker> progressionMarkers;
        public List<SPBundle> bundles;
        public List<SPCurrency> currencies;
    }

    public class SPGrantRewardsResult : SpecterApiResultBase<SPGrantRewardsResponseData>
    {
        public List<SpecterInventoryItem> InventoryItemList;
        public List<SpecterWalletCurrency> WalletCurrencyList;
        public List<SpecterUpdatedUserProgress> Progressions;
        public List<SpecterInventoryBundle> InventoryBundleList;

        protected override void InitSpecterObjectsInternal()
        {
            InventoryItemList = new List<SpecterInventoryItem>();
            WalletCurrencyList = new List<SpecterWalletCurrency>();
            Progressions = new List<SpecterUpdatedUserProgress>();
            InventoryBundleList = new List<SpecterInventoryBundle>();

            foreach (var inventoryItem in Response.data.items)
            {
                InventoryItemList.Add(new SpecterInventoryItem(inventoryItem));
            }

            foreach (var currency in Response.data.currencies)
            {
                WalletCurrencyList.Add(new SpecterWalletCurrency(currency));
            }

            foreach (var progress in Response.data.progress)
            {
                Progressions.Add(new SpecterUpdatedUserProgress(progress));
            }

            foreach (var bundle in Response.data.bundles)
            {
                InventoryBundleList.Add(new SpecterInventoryBundle(bundle));
            }
        }
    }

    public partial class SPRewardsApiClient
    {
        public async Task<SPGrantRewardsResult> GrantRewardsTaskAsync(SPGrantRewardsRequest request)
        {
            var result = await PostAsync<SPGrantRewardsResult, SPGrantRewardsResponseData>("/v1/client/rewards/grant", AuthType, request);
            return result;
        }
    }
}