using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPGetOtherPlayerWalletHistoryResponse : List<SPWalletHistoryEntryData> ,ISpecterApiResponseData
    {
    }
}