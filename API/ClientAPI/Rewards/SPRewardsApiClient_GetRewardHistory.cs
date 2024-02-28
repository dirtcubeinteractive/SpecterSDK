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
    /// Represents a request to get a user's reward history from the Specter Rewards API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetRewardsHistoryRequest"/> class represents a request to get a user's reward history from the Specter Stores API.
    /// It can be used to specify the filter criteria for the reward history information to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the <b>GetRewardHistoryAsync</b> method in the <see cref="SPRewardsApiClient"/> class to retrieve the reward history details from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetRewardsHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Set this property to filter and fetch reward history entries by their status.
        /// See <see cref="SPRewardClaimStatus"/> for possible reward status filters.
        /// </summary>
        public SPRewardClaimStatus status { get; set; }

        /// <summary>
        /// Set this property to filter and fetch reward history entries based on their grant type.
        /// See <see cref="SPRewardGrantType"/> for possible options.
        /// </summary>
        public SPRewardGrantType rewardGrant { get; set; }

        /// <summary>
        /// List representing a list of task IDs to filter and fetch only reward history entries that were created on a user's
        /// completion of tasks.
        /// </summary>
        public List<string> taskIds { get; set; }

        /// <summary>
        /// List representing a list of task group IDs to filter and fetch only reward history entries that were created on a user's
        /// completion of task groups.
        /// </summary>
        public List<string> taskGroupIds { get; set; }

        /// <summary>
        /// List representing a list of level IDs to filter and fetch only reward history entries that were created on a user's
        /// completion of a level in any progression system.
        /// </summary>
        public List<string> levelIds { get; set; }

        /// <summary>
        /// Optional entities configuration to retrieve only a specific type of reward history entries.
        /// See the Get Reward History route in the Rewards section of the
        /// <a href="https://doc.specterapp.xyz">Specter API Doc</a> for info about possible entities.
        /// <example>
        /// If you only want to retrieve item reward entries. You would use the "items" entity.
        /// </example>
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetRewardHistoryAsync method in the <see cref="SPRewardsApiClient"/> class.
    /// </summary>
    public class SPGetRewardsHistoryResult : SpecterApiResultBase<SPGetRewardHistoryResponseData>
    {
        /// <summary>
        /// List of <see cref="SpecterRewardHistoryEntry"/> representing encapsulated info about item rewards.
        /// </summary>
        public List<SpecterRewardHistoryEntry> Items;
        
        /// <summary>
        /// List of <see cref="SpecterRewardHistoryEntry"/> representing encapsulated info about bundle rewards.
        /// </summary>
        public List<SpecterRewardHistoryEntry> Bundles;
        
        /// <summary>
        /// List of <see cref="SpecterRewardHistoryEntry"/> representing encapsulated info about currency rewards.
        /// </summary>
        public List<SpecterRewardHistoryEntry> Currencies;
        
        /// <summary>
        /// List of <see cref="SpecterRewardHistoryEntry"/> representing encapsulated info about progression marker rewards.
        /// </summary>
        public List<SpecterRewardHistoryEntry> ProgressionMarkers;
        
        /// <summary>
        /// Map of consolidated <see cref="SpecterRewardHistoryDetails"/> based on the reward source type and source ID.
        /// <remarks>
        /// This is a convenience data structure to easily access reward information for a particular reward source.
        /// </remarks>
        /// <seealso cref="SPRewardSourceType"/>
        /// </summary>
        public Dictionary<SPRewardSourceType, Dictionary<string, SpecterRewardHistoryDetails>> RewardsMap;

        protected override void InitSpecterObjectsInternal()
        {
            RewardsMap = new Dictionary<SPRewardSourceType, Dictionary<string, SpecterRewardHistoryDetails>>
            {
                { SPRewardSourceType.Level, new Dictionary<string, SpecterRewardHistoryDetails>() },
                { SPRewardSourceType.Task, new Dictionary<string, SpecterRewardHistoryDetails>() },
                { SPRewardSourceType.TaskGroup, new Dictionary<string, SpecterRewardHistoryDetails>() },
                { SPRewardSourceType.Custom, new Dictionary<string, SpecterRewardHistoryDetails>() }
            };

            Items = new List<SpecterRewardHistoryEntry>(); 
            foreach (var item in Response.data.items)
            {
                var itemEntry = GetEntryAndAddToMap(item, SPRewardType.Item);
                Items.Add(itemEntry);
            }
            
            Bundles = new List<SpecterRewardHistoryEntry>();
            foreach (var bundle in Response.data.bundles)
            {
                var bundleEntry = GetEntryAndAddToMap(bundle, SPRewardType.Bundle);
                Bundles.Add(bundleEntry);
            }
            
            Currencies = new List<SpecterRewardHistoryEntry>();
            foreach (var currency in Response.data.currencies)
            {
                var currencyEntry = GetEntryAndAddToMap(currency, SPRewardType.Currency);
                Currencies.Add(currencyEntry);
            }
            
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
            foreach (var progress in Response.data.progressionMarkers)
            {
                var progressionMarkerEntry = GetEntryAndAddToMap(progress, SPRewardType.ProgressionMarker);
                ProgressionMarkers.Add(progressionMarkerEntry);
            }
        }


        private SpecterRewardHistoryEntry GetEntryAndAddToMap (SPRewardHistoryEntryData entryData, SPRewardType rewardType)
        {
            SpecterRewardHistoryEntry rewardHistoryEntry = new SpecterRewardHistoryEntry(entryData, rewardType);

            if (!RewardsMap[rewardHistoryEntry.SourceType].ContainsKey(rewardHistoryEntry.SourceId))
            {
                SpecterRewardHistoryDetails specterRewards = new SpecterRewardHistoryDetails(rewardHistoryEntry.SourceId, rewardHistoryEntry.SourceType, rewardHistoryEntry.Status, rewardHistoryEntry.RewardGrant);
                switch (rewardType)
                {
                    case SPRewardType.ProgressionMarker:
                        specterRewards.ProgressionMarkers.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Currency:
                        specterRewards.Currencies.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Item:
                        specterRewards.Items.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Bundle:
                        specterRewards.Bundles.Add(rewardHistoryEntry);
                        break;
                    default:
                        break;
                } 
                RewardsMap[rewardHistoryEntry.SourceType].Add(rewardHistoryEntry.SourceId, specterRewards);
            }
            else
            {
                switch (rewardType)
                {
                    case SPRewardType.ProgressionMarker:
                        RewardsMap[rewardHistoryEntry.SourceType][rewardHistoryEntry.SourceId].ProgressionMarkers.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Currency:
                        RewardsMap[rewardHistoryEntry.SourceType][rewardHistoryEntry.SourceId].Currencies.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Item:
                        RewardsMap[rewardHistoryEntry.SourceType][rewardHistoryEntry.SourceId].Items.Add(rewardHistoryEntry);
                        break;
                    case SPRewardType.Bundle:
                        RewardsMap[rewardHistoryEntry.SourceType][rewardHistoryEntry.SourceId].Bundles.Add(rewardHistoryEntry);
                        break;
                    default:
                        break;
                }
            }
            return rewardHistoryEntry;
        }
    }

    public partial class SPRewardsApiClient
    {
        /// <summary>
        /// Get the user's reward history asynchronously.
        /// </summary>
        /// <remarks>
        /// For full information about the get reward history endpoint, see the Rewards section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetRewardsHistoryRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetRewardsHistoryResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetRewardsHistoryResult> GetRewardHistoryAsync(SPGetRewardsHistoryRequest request)
        {
            var result = await PostAsync<SPGetRewardsHistoryResult, SPGetRewardHistoryResponseData>("/v1/client/rewards/get-history", AuthType, request);
            return result;
        }
    }
}