using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    public partial class SPMePlayerApi
    {
        private SpecterApiClientBase m_Client;

        public SPMePlayerApi(SpecterApiClientBase client)
        {
            m_Client = client;
        }
    }
}