using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    public class SPCheckAttemptsResponse : ISpecterApiResponseData
    {
        public int numberOfAttemptsLeft { get; set; }
    }
}