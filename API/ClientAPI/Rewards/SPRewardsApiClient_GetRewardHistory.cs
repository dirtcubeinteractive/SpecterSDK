using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetRewardsHistoryRequest : SPApiRequestBase
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
        public List<SpecterCurrencyRewardHistoryEntry> Currencies;
        public List<SpecterRewardHistoryEntry> ProgressionMarkers;

        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterRewardHistoryEntry>();
            Bundles = new List<SpecterRewardHistoryEntry>();
            Currencies = new List<SpecterCurrencyRewardHistoryEntry>();
            ProgressionMarkers = new List<SpecterRewardHistoryEntry>();
            foreach (var item in Response.data.items)
            {
                Items.Add(new SpecterRewardHistoryEntry(item));
            }
            foreach (var bundle in Response.data.bundles)
            {
                Bundles.Add(new SpecterRewardHistoryEntry(bundle));
            }
            foreach (var currency in Response.data.currencies)
            {
                Currencies.Add(new SpecterCurrencyRewardHistoryEntry(currency));
            }
            foreach (var progress in Response.data.progressionMarkers)
            {
                ProgressionMarkers.Add(new SpecterRewardHistoryEntry(progress));
            }
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