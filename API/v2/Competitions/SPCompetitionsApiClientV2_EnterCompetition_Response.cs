using System;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    [Serializable]
    public class SPEnterCompetitionResponse : ISpecterApiResponseData
    {
        public string entryId { get; set; }
        public string competitionInstanceId { get; set; }
    }
}