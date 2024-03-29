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
    /// <summary>
    /// Represents a request to grant rewards to a user/player.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardsRequest : SPApiRequestBase
    {
        /// <summary>
        /// List of reward details according to their respective sources.
        /// <seealso cref="SPRewardGrantInfo"/>
        /// <seealso cref="SPRewardSourceInfo"/>
        /// </summary>
        public List<SPRewardGrantInfo> rewardDetails;
    }

    /// <summary>
    /// Represents information of rewards to grant for a single source.
    /// <seealso cref="SPRewardSourceInfo"/>
    /// <seealso cref="SPRewardSourceType"/>
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardGrantInfo
    {
        /// <summary>
        /// An instance of <see cref="SPRewardSourceInfo"/> to represent the source of the reward.
        /// </summary>
        public SPRewardSourceInfo source;
        
        /// <summary>
        /// The <see cref="SPRewardsToGrant"/> to the user.
        /// Setting the rewards property is necessary only when granting custom rewards, or to
        /// override the reward configuration for the specified source, for example, if you want to grant double
        /// rewards for a user having watched a rewarded video ad, you would construct an instance of
        /// <see cref="SPRewardsToGrant"/> with the relevant <see cref="SPRewardedResourceInfoBase"/>s, double the amounts
        /// of the rewards that you wish to grant and set
        /// this property.
        /// </summary>
        public SPRewardsToGrant rewards;
        
        /// <summary>
        /// Flag to tell the server to ignore any access and eligibility configurations for any of the rewards (eg: progression or item locks)
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to tell the server to bypass any limited edition configurations only applicable for item and bundle rewards.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
        
        /// <summary>
        /// <para>
        /// A for any meta data you wish to store for the granted rewards.
        /// this info is relevant when retrieving the reward history for any custom needs.
        /// </para>
        /// <para>
        /// It would typically be needed mainly when granting rewards with source type set to <b>"Custom"</b>
        /// </para>
        /// <example>
        /// If you grant a custom reward, then you may want to store an identifier for the source in meta as
        /// we do not store any IDs for custom rewards.
        /// </example>
        /// <seealso cref="SPRewardSourceType"/>
        /// <seealso cref="SPRewardSourceInfo"/>
        /// </summary>
        public Dictionary<string, object> meta;
    }
    
    /// <summary>
    /// A class representing the source of a specific set of rewards.
    /// <remarks>
    /// A reward source is info about the reason for which a player earned a set of rewards. For all source types except custom,
    /// Specter keeps track of the source ID which can be retried by getting the reward history for a user. 
    /// </remarks>
    /// <seealso cref="SPRewardSourceType"/>
    /// <seealso cref="SPRewardsApiClient.GetRewardHistoryAsync"/>
    /// </summary>
    [Serializable]
    public class SPRewardSourceInfo
    {
        /// <summary>
        /// The ID of the source.
        /// <example>For example, rewards for a task would have source ID equal to the dashboard specific ID of the task</example>
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// ID of the instance of the reward set if the same source could result in rewards multiple times (eg: recurring tasks)
        /// This ID is retrieved when fetching the reward history.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// The type of the source. See <see cref="SPRewardSourceType"/> for possible reward source types.
        /// </summary>
        public SPRewardSourceType type { get; set; }
    }

    /// <summary>
    /// Base class for representing rewarded resources.
    /// This class is needed mainly when granting a reward for a custom source, or when overriding
    /// the configured rewards for a particular reward source.
    /// <seealso cref="SPRewardGrantInfo"/>
    /// <see cref="SPRewardsToGrant"/>
    /// </summary>
    [Serializable]
    public abstract class SPRewardedResourceInfoBase
    {
        /// <summary>
        /// The dashboard specified ID of the resource.
        /// </summary>
        public string id;
        
        /// <summary>
        /// The amount of the resource to be granted as a reward.
        /// </summary>
        public int amount;
        
        /// <summary>
        /// Flag to tell the server to ignore any access and eligibility configurations for this specific reward (eg: progression or item locks)
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to tell the server to bypass any limited edition configurations for this reward. Only applicable for item and bundle rewards.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
    }

    /// <summary>
    /// Represents info about a rewarded item.
    /// </summary>
    [Serializable]
    public class SPRewardedItemInfo : SPRewardedResourceInfoBase
    {
        /// <summary>
        /// The ID of the collection within the player's inventory in which the rewarded item must
        /// be added. See <see cref="SpecterSDK.API.ClientAPI.Inventory.SPInventoryApiClient.SPInventoryEntityInfo"/> for
        /// more info about collection IDs.
        /// </summary>
        public string collectionId;
        
        public SPRewardedItemInfo(string id,int amount ,string collectionId)
        {
            this.id = id;
            this.amount = amount;
            this.collectionId = collectionId;
        }
    }
    
    /// <summary>
    /// Represents info about a rewarded bundle.
    /// </summary>
    [Serializable]
    public class SPRewardedBundleInfo : SPRewardedItemInfo
    {
        public SPRewardedBundleInfo(string id, int amount, string collectionId) : base(id,amount,collectionId)
        {}
    }
    
    /// <summary>
    /// Represents info about a rewarded progression marker.
    /// </summary>
    [Serializable]
    public class SPRewardedProgressionMarkerInfo : SPRewardedResourceInfoBase
    {
        public SPRewardedProgressionMarkerInfo(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
    
    /// <summary>
    /// Represents info about a rewarded currency.
    /// </summary>
    [Serializable]
    public class SPRewardedCurrencyInfo : SPRewardedResourceInfoBase
    {
        public SPRewardedCurrencyInfo(string id, int amount)
        {
           this.id = id;
           this.amount = amount;
        }
    }
    
    /// <summary>
    /// Represents a collection of rewards to grant to a user/player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardsToGrant
    {
        /// <summary>
        /// A list of <see cref="SPRewardedItemInfo"/>s to grant.
        /// </summary>
        public List<SPRewardedItemInfo> items;
        
        /// <summary>
        /// A list of <see cref="SPRewardedBundleInfo"/>s to grant.
        /// </summary>
        public List<SPRewardedBundleInfo> bundles;
        
        /// <summary>
        /// A list of <see cref="SPRewardedProgressionMarkerInfo"/>s to grant.
        /// </summary>
        public List<SPRewardedProgressionMarkerInfo> progressionMarkers;
        
        /// <summary>
        /// A list of <see cref="SPRewardedCurrencyInfo"/>s to grant.
        /// </summary>
        public List<SPRewardedCurrencyInfo> currencies;
    }

    /// <summary>
    /// Represents the result of the GrantRewardsAsync operation.
    /// </summary>
    public class SPGrantRewardsResult : SpecterApiResultBase<SPGrantRewardsResponseData>
    {
        // List of inventory items that were added to the user's inventory due to the grant.
        public List<SpecterInventoryItem> InventoryItemList;
        
        // List of inventory bundles that were added to the user's inventory due to the grant.
        public List<SpecterInventoryBundle> InventoryBundleList;
        
        // List of wallet currencies that were added to the user's respective currency wallets due to the grant.
        public List<SpecterWalletCurrency> WalletCurrencyList;
        
        // List of progressions that were updated for the user due to the grant.
        public List<SpecterUserProgress> Progressions;

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
        /// <summary>
        /// Grant rewards to a user/player asynchronously.
        /// /// <remarks>
        /// For full information about the grant rewards endpoint, see the Rewards section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// <see cref="SPGrantRewardsRequest"/> containing details about the default purchase to be made.
        /// </param>
        /// <returns>
        /// A Task representing the asynchronous operation, with <see cref="SPGrantRewardsResult"/> as the result type.
        /// </returns>
        public async Task<SPGrantRewardsResult> GrantRewardsAsync(SPGrantRewardsRequest request)
        {
            var result = await PostAsync<SPGrantRewardsResult, SPGrantRewardsResponseData>("/v1/client/rewards/grant", AuthType, request);
            return result;
        }
    }
}