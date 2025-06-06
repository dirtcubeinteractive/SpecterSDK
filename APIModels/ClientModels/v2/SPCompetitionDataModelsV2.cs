using System;

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
}