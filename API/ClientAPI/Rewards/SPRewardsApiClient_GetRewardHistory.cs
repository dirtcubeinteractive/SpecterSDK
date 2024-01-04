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
    public class SPGetRewardsHistoryRequest : SPPaginatedApiRequest
    {
        public string status { get; set; }

        public string rewardGrant { get; set; }

        public List<string> taskIds { get; set; }

        public List<string> taskGroupIds { get; set; }

        public List<string> progressionSystemIds { get; set; }

        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetRewardsHistoryResult : SpecterApiResultBase<SPGetRewardHistoryResponseData>
    {
        public List<SpecterRewardHistoryEntry> Items;
        public List<SpecterRewardHistoryEntry> Bundles;
        public List<SpecterRewardHistoryEntry> Currencies;
        public List<SpecterRewardHistoryEntry> ProgressionMarkers;

        public Dictionary<SPRewardSourceType, Dictionary<string, SpecterRewards>> RewardsMap;

        protected override void InitSpecterObjectsInternal()
        {
            RewardsMap = new Dictionary<SPRewardSourceType, Dictionary<string, SpecterRewards>>
            {
                { SPRewardSourceType.ProgressionSystem, new Dictionary<string, SpecterRewards>() },
                { SPRewardSourceType.Task, new Dictionary<string, SpecterRewards>() },
                { SPRewardSourceType.TaskGroup, new Dictionary<string, SpecterRewards>() }
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
                SpecterRewards specterRewards = new SpecterRewards(rewardHistoryEntry.SourceId, rewardHistoryEntry.SubSourceId, rewardHistoryEntry.SourceType, rewardHistoryEntry.Status, rewardHistoryEntry.RewardGrant);
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
        public async Task<SPGetRewardsHistoryResult> GetRewardHistoryAsync(SPGetRewardsHistoryRequest request)
        {
            var result = await PostAsync<SPGetRewardsHistoryResult, SPGetRewardHistoryResponseData>("/v1/client/rewards/get-history", AuthType, request);
            return result;
        }
    }
}