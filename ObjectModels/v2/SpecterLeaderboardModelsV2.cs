using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPLeaderboardResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPLeaderboardResource() { }
        public SPLeaderboardResource(SPLeaderboardResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }

    public class SPLeaderboard : ISpecterResource, ISpecterMasterObject, ISpecterLeaderboard, ISpecterLiveOpsEntity
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPSchedule Schedule { get; set; }
        
        public SPMatchResource Match { get; set; }
        public SPRankingMethod RankingMethod { get; set; }
        public SPLeaderboardSourceType Source { get; set; }
        public SPPrizeDistribution PrizeDistribution { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPLeaderboard() { }
        public SPLeaderboard(SPLeaderboardData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Schedule = new SPSchedule(data.schedule);
            
            Match = data.match == null ? null : new SPMatchResource(data.match);
            RankingMethod = data.rankingMethod.id;
            Source = data.sourceType.id;
            PrizeDistribution = data.prizeDistribution == null ? null : new SPPrizeDistribution(data.prizeDistribution, SPRewardSourceType.Leaderboard);
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }
}