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
    public class SPGrantRewardsRequest : SPApiRequestBase
    {
        public List<SPRewardGrantInfo> rewardDetails;
    }

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardGrantInfo
    {
        public SPRewardSourceInfo source;
        public SPRewardsToGrant rewards;
        public Dictionary<string, object> meta;
    }

    [Serializable]
    public class SPRewardSourceInfo
    {
        public string id { get; set; }
        public SPRewardSourceType type { get; set; }
    }
    public abstract class SPRewardedResourceInfoBase
    {
        public string id;
        public int amount;
    }
    public class SPRewardedItemInfo : SPRewardedResourceInfoBase
    {
        public string collectionId;
        public SPRewardedItemInfo(string id,int amount ,string collectionId)
        {
            this.id = id;
            this.amount = amount;
            this.collectionId = collectionId;
        }
    }
    public class SPRewardedBundleInfo : SPRewardedItemInfo
    {
        public SPRewardedBundleInfo(string id, int amount, string collectionId) : base(id,amount,collectionId)
        {}
    }
    public class SPRewardedProgressionMarkerInfo : SPRewardedResourceInfoBase
    {
        public SPRewardedProgressionMarkerInfo(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
    public class SPRewardedCurrencyInfo : SPRewardedResourceInfoBase
    {
        public SPRewardedCurrencyInfo(string id, int amount)
        {
           this.id = id;
           this.amount = amount;
        }
    }
    
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardsToGrant
    {
        public List<SPRewardedItemInfo> items;
        public List<SPRewardedProgressionMarkerInfo> progressionMarkers;
        public List<SPRewardedBundleInfo> bundles;
        public List<SPRewardedCurrencyInfo> currencies;
    }

    public class SPGrantRewardsResult : SpecterApiResultBase<SPGrantRewardsResponseData>
    {
        public List<SpecterInventoryItem> InventoryItemList;
        public List<SpecterWalletCurrency> WalletCurrencyList;
        public List<SpecterUserProgress> Progressions;
        public List<SpecterInventoryBundle> InventoryBundleList;

        protected override void InitSpecterObjectsInternal()
        {
            InventoryItemList = new List<SpecterInventoryItem>();
            WalletCurrencyList = new List<SpecterWalletCurrency>();
            Progressions = new List<SpecterUserProgress>();
            InventoryBundleList = new List<SpecterInventoryBundle>();

            foreach (var inventoryItem in Response.data.items)
            {
                InventoryItemList.Add(new SpecterInventoryItem(inventoryItem));
            }

            foreach (var currency in Response.data.currencies)
            {
                WalletCurrencyList.Add(new SpecterWalletCurrency(currency));
            }

            foreach (var progress in Response.data.progressionMarkers)
            {
                Progressions.Add(new SpecterUserProgress(progress));
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