using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyPlayerDataResponse : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData
    {
    }
}