using System;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPCompetitionConfigData
    {
        public long? minPlayers { get; set; }
        public long? maxPlayers { get; set; }
        public long? maxEntryAllowed { get; set; }
        public long? maxAttemptAllowed { get; set; }
    }
    
    [Serializable]
    public class SPCompetitionEntryDataV2
    {
        public string entryId { get; set; }
        public long numberOfAttemptsLeft { get; set; }
        public SPCompetitionEntryStatus status { get; set; }
        public SPInstanceScheduleData instanceSchedule { get; set; }
    }
}