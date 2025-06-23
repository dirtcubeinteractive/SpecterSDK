using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.API.v2.Matches;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.APIModels.ClientModels;
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
    
    public class SPMatch : ISpecterMatchInfo, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        
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

            MinPlayers = data.minPlayers ?? (data.formatType.id != SPMatchFormatType.SinglePlayer ? 2 : 1);
            MaxPlayers = data.maxPlayers ?? MinPlayers;
            
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

    public class SPMatchHistoryEntry : ISpecterMatchInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPGameResource Game { get; set; }
        public SPCompetitionResource Competition { get; set; }
        
        public string MatchSessionId { get; set; }
        public DateTime PlayedAt { get; set; }
        public long Score { get; set; }
        
        public List<SPMatchParticipantInfo> PlayerDetails { get; set; }
        
        public bool WasCompetition => Competition != null;
        
        public SPMatchHistoryEntry() { }
        public SPMatchHistoryEntry(SPMatchHistoryEntryData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Game = new SPGameResource(data.game);
            
            Competition = data.competition == null ? null : new SPCompetitionResource(data.competition);
            
            MatchSessionId = data.matchSessionId;
            PlayedAt = data.playedAt;
            Score = data.score;
            
            PlayerDetails = data.playerDetails?.ConvertAll(x => new SPMatchParticipantInfo(x));
        }
    }

    public class SPMatchParticipantInfo : ISpecterBaseUserProfile
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ThumbUrl { get; set; }
        
        public long Score { get; set; }
        public int Rank { get; set; }
        
        public SPMatchParticipantInfo() { }
        public SPMatchParticipantInfo(SPMatchParticipantData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            FirstName = data.firstName;
            LastName = data.lastName;
            Username = data.username;
            DisplayName = data.displayName;
            ThumbUrl = data.thumbUrl;
            Score = data.score;
            Rank = data.rank;
        }
    }

    public class SPMatchSessionResultInfo
    {
        public string MatchSessionId { get; set; }
        
        public SPMatchResource Match { get; set; }
        public SPGameResource Game { get; set; }
        public SPCompetitionResource Competition { get; set; }
        
        public List<SPMatchSessionPlayerInfo> UserInfos { get; set; }
        
        public DateTime? PlayedAt { get; set; }
        
        public SPMatchSessionResultInfo() { }
        public SPMatchSessionResultInfo(SPMatchSessionResultData data)
        {
            MatchSessionId = data.matchSessionId;
            Match = new SPMatchResource(data.match);
            Game = new SPGameResource(data.game);
            Competition = data.competition == null ? null : new SPCompetitionResource(data.competition);
            
            UserInfos = data.userInfo?.ConvertAll(x => new SPMatchSessionPlayerInfo(x)) ?? new List<SPMatchSessionPlayerInfo>();
            PlayedAt = data.playedAt;
        }
    }

    public class SPMatchSessionPlayerInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string EntryId { get; set; }
        public long Score { get; set; }
        public int Rank { get; set; }
        
        public SPMatchSessionPlayerInfo() { }
        public SPMatchSessionPlayerInfo(SPMatchSessionPlayerInfoData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            EntryId = data.entryId;
            Score = data.score;
            Rank = data.rank;
        }
    }
}