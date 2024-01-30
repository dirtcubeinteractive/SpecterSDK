using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterMatchBase : SpecterResource
    {
        public SpecterMatchBase(SPMatchResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            IconUrl = data.iconUrl;
            Description = data.description;
        }
    }

    public class SpecterMatch : SpecterMatchBase , ISpecterMasterObject
    {
        public string HowTo;
        public int MinPlayers;
        public int MaxPlayers;
        public int? NumberOfPositions;
        public int DefaultOutcomeValue;
        public SpecterGameBase Game;
        public SPMatchFormatType MatchFormatType;
        public SPGameMatchOutcomeType GameMatchOutcomeType;
        public List<SpecterLeaderboardInfo> Leaderboards; 
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SpecterMatch(SPMatchResponseData data) : base(data)
        {
            HowTo = data.howTo;
            MinPlayers = data.minPlayers;
            MaxPlayers = data.maxPlayers;
            NumberOfPositions = data.numberOfPositions;
            DefaultOutcomeValue = data.defaultOutcomeValue;
            MatchFormatType = data.matchFormatType.name;
            GameMatchOutcomeType = data.matchOutcomeType.name;

            if (data.game != null)
                Game = new SpecterGameBase(data.game);
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, string>();
            Leaderboards = new List<SpecterLeaderboardInfo>();
            foreach (var leaderBoard in data.leaderboards)
            {
               Leaderboards.Add(new SpecterLeaderboardInfo(leaderBoard));
            } 
        }
    }
    public class SpecterLeaderboardInfo
    {
        public string Uuid;
        public string Id;
        public string Name;
        public string Description;
        
        public SpecterLeaderboardInfo(SPLeaderboardInfoResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
        }
    }
}