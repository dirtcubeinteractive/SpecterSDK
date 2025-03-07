using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Players.Others
{
    public partial class SPOtherPlayerApi
    {
        private SpecterApiClientBase m_Client;
        
        public SPOtherPlayerApi(SpecterApiClientBase client)
        {
            m_Client = client;
        }
    }
}