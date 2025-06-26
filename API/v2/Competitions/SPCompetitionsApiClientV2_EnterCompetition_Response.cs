using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    public class SPEnterCompetitionResponse : ISpecterApiResponseData
    {
        public string entryId { get; set; }
        public string competitionInstanceId { get; set; }
    }
}