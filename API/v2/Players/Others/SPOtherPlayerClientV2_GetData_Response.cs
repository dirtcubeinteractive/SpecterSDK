using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPGetOtherPlayerDataResponse : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData
    {
    }
}