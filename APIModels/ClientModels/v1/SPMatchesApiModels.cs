using System;
using System.Collections.Generic;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.APIModels.ClientModels.v1
{
    [Serializable]
    public class SPMatchSessionResponseData : ISpecterApiResponseData
    {
        public string matchSessionId { get; set; }
    }
    
    [Serializable]
    public class SPMatchResponseBaseData : SPResourceResponseData
    {
    }

    [Serializable]
    public class SPMatchResponseData : SPMatchResponseBaseData, ISpecterMasterData
    {
        public int minPlayers { get; set; }
        public int maxPlayers { get; set; }
        public string howTo { get; set; }
        public int numberOfPositions { get; set; }
        public int defaultOutcomeValue { get; set; }
        public SPGameResponseBaseData game { get; set; }
        public SPMatchFormatData matchFormatType { get; set; }
        public SPGameMatchOutcomeData matchOutcomeType { get; set; }
        public List<SPLeaderboardInfoResponseData> leaderboards { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPMatchFormatData
    {
        public SPMatchFormatType id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class SPMatchWinConditionData
    {
        public SPMatchWinCondition id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class SPGameMatchOutcomeData
    {
        public SPGameMatchOutcomeType id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class SPLeaderboardInfoResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    [Serializable]
    public class SPGetMatchesResponseData : ISpecterApiResponseData
    {
        public List<SPMatchResponseData> matches { get; set; }
        public int totalCount { get; set; }
    }

}
