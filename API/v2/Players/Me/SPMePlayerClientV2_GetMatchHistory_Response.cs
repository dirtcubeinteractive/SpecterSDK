using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyMatchHistoryResponse : List<SPMatchHistoryEntryData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPMatchHistoryEntryData : ISpecterMatchInfoData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPGameResourceData game { get; set; }
        public SPCompetitionResourceData competition { get; set; }
        
        public string matchSessionId { get; set; }
        public DateTime playedAt { get; set; }
        public long score { get; set; }
        
        public List<SPMatchParticipantData> playerDetails { get; set; }
    }

    [Serializable]
    public class SPMatchParticipantData : ISpecterBaseUserProfileData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string username { get; set; }
        public string thumbUrl { get; set; }
        
        public long score { get; set; }
        public int rank { get; set; }
    }
}