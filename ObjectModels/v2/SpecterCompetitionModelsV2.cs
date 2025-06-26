using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPCompetitionResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPCompetitionResource() { }
        public SPCompetitionResource(SPCompetitionResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    
    public class SPCompetitionConfig
    {
        public long? MinPlayers { get; set; }
        public long? MaxPlayers { get; set; }
        public long? MaxEntryAllowed { get; set; }
        public long? MaxAttemptAllowed { get; set; }
        
        public SPCompetitionConfig() { }
        public SPCompetitionConfig(SPCompetitionConfigData data)
        {
            MinPlayers = data.minPlayers;
            MaxPlayers = data.maxPlayers;
            MaxEntryAllowed = data.maxEntryAllowed;
            MaxAttemptAllowed = data.maxAttemptAllowed;
        }
    }

    public class SPInstantBattle : ISpecterResource, ISpecterMasterObject, ISpecterUnlockable, ISpecterCompetition, ISpecterCompetitionInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPSchedule Schedule { get; set; }
        
        public SPCompetitionConfig Config { get; set; }
        public SPCompetitionFormat Type { get; set; }
        public List<SPEntryFeeInfo> EntryFees { get; set; }
        
        public SPRankingMethod RankingMethod { get; set; }
        public SPLeaderboardSourceType Source { get; set; }
        public SPMatchResource Match { get; set; }
        public SPPrizeDistribution PrizeDistribution { get; set; }
        
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPInstantBattle() { }
        public SPInstantBattle(SPInstantBattleData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Schedule = data.schedule == null ? null : new SPSchedule(data.schedule);
            
            Config = new SPCompetitionConfig(data.config);
            Type = data.type.id;
            EntryFees = data.entryFees?.ConvertAll(x => new SPEntryFeeInfo(x));
            
            RankingMethod = data?.rankingMethod?.id;
            Source = data.source.id;
            Match = new SPMatchResource(data.match);
            PrizeDistribution = data.prizeDistribution == null ? null : new SPPrizeDistribution(data.prizeDistribution, SPRewardSourceType.InstantBattle);
            
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPTournament : ISpecterResource, ISpecterMasterObject, ISpecterUnlockable, ISpecterCompetition, ISpecterCompetitionInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPSchedule Schedule { get; set; }
        
        public SPCompetitionConfig Config { get; set; }
        public SPCompetitionFormat Type { get; set; }
        public List<SPEntryFeeInfo> EntryFees { get; set; }

        public SPRankingMethod RankingMethod { get; set; }
        public SPLeaderboardSourceType Source { get; set; }
        public SPMatchResource Match { get; set; }
        public SPPrizeDistribution PrizeDistribution { get; set; }
        
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPTournament() { }
        public SPTournament(SPTournamentData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Schedule = data.schedule == null ? null : new SPSchedule(data.schedule);
            
            Config = new SPCompetitionConfig(data.config);
            Type = data.type.id;
            EntryFees = data.entryFees?.ConvertAll(x => new SPEntryFeeInfo(x));

            RankingMethod = data?.rankingMethod?.id;
            Source = data.source.id;
            Match = new SPMatchResource(data.match);
            PrizeDistribution = data.prizeDistribution == null ? null : new SPPrizeDistribution(data.prizeDistribution, SPRewardSourceType.Tournament);
            
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }
    
    public class SPCompetitionEntryInfo
    {
        public string EntryId { get; set; }
        public long? NumberOfAttemptsLeft { get; set; }
        public SPCompetitionEntryStatus Status { get; set; }
        public SPInstanceSchedule InstanceSchedule { get; set; }
        
        public bool HasAttempts => NumberOfAttemptsLeft == null || NumberOfAttemptsLeft > 0;
        
        public SPCompetitionEntryInfo() { }
        public SPCompetitionEntryInfo(SPCompetitionEntryDataV2 data)
        {
            EntryId = data.entryId;
            NumberOfAttemptsLeft = data.numberOfAttemptsLeft;
            Status = data.status;
            InstanceSchedule = data.instanceSchedule == null ? null : new SPInstanceSchedule(data.instanceSchedule);
        }
    }

    public class SPInstantBattleHistoryEntry : ISpecterResource, ISpecterCompetitionHistoryEntry
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPLeaderboardSourceType Source { get; set; }
        public SPMatchResource Match { get; set; }
        public SPCompetitionConfig Config { get; set; }
        public SPCompetitionFormat Type { get; set; }
        
        public List<SPCompetitionEntryInfo> EntryDetails { get; set; }
        
        public SPInstantBattleHistoryEntry() { }
        public SPInstantBattleHistoryEntry(SPInstantBattleHistoryEntryData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Source = data.source?.id;
            Match = data.match == null ? null : new SPMatchResource(data.match);
            Config = data.config == null ? null : new SPCompetitionConfig(data.config);
            Type = data.type?.id;
            
            EntryDetails = data.entryDetails?.ConvertAll(x => new SPCompetitionEntryInfo(x)) ?? new List<SPCompetitionEntryInfo>();
        }
    }

    public class SPTournamentHistoryEntry : ISpecterResource, ISpecterCompetitionHistoryEntry
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPLeaderboardSourceType Source { get; set; }
        public SPMatchResource Match { get; set; }
        public SPCompetitionConfig Config { get; set; }
        public SPCompetitionFormat Type { get; set; }
        
        public List<SPCompetitionEntryInfo> EntryDetails { get; set; }
        
        public SPTournamentHistoryEntry() { }
        public SPTournamentHistoryEntry(SPTournamentHistoryEntryData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Source = data.source?.id;
            Match = data.match == null ? null : new SPMatchResource(data.match);
            Config = data.config == null ? null : new SPCompetitionConfig(data.config);
            Type = data.type?.id;
            
            EntryDetails = data.entryDetails?.ConvertAll(x => new SPCompetitionEntryInfo(x)) ?? new List<SPCompetitionEntryInfo>();
        }
    }
}