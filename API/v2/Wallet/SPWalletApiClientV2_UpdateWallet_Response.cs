using System;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Wallet
{
    [Serializable]
    public class SPUpdateWalletResponse : SPWalletCurrencyData, ISpecterApiResponseData
    {
        public long requested { get; set; }
        public long applied { get; set; }
        public bool wasAdjusted { get; set; }
        public string adjustmentReason { get; set; }
    }
}