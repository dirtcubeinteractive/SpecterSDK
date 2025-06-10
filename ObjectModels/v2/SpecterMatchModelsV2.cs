using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPMatchResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        public SPMatchResource() { }
        public SPMatchResource(SPMatchResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    
    public class SPMatch : ISpecterResource, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        // TODO: Ensure min and max players is added to Get Matches response
        /*public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }*/
        
        public SPGameResource Game { get; set; }
        public SPMatchFormatType Format { get; set; }
        public SPMatchOutcomeType OutcomeType { get; set; }
        public SPMatchWinCondition WinCondition { get; set; }
        
        public List<SPLeaderboardResource> Leaderboards { get; set; }
        public List<SPCompetitionResource> Competitions { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPMatch() { }
        public SPMatch(SPMatchData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            /*MinPlayers = data.minPlayers ?? (data.formatType.id != SPMatchFormatType.SinglePlayer ? 2 : 1);
            MaxPlayers = data.maxPlayers ?? MinPlayers;*/
            
            Game = new SPGameResource(data.game);
            Format = data.formatType?.id;
            OutcomeType = data.outcomeType?.id;
            WinCondition = data.winCondition?.id;
            
            Leaderboards = data.leaderboards?.ConvertAll(x => new SPLeaderboardResource(x));
            Competitions = data.competitions?.ConvertAll(x => new SPCompetitionResource(x));
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }
}