using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPUpdateOtherPlayerDataResponse : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData
    {
    }
}