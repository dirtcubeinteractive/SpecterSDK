using System;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Progression
{
    [Serializable]
    public class SPUpdateProgressionMarkerResponse : SPMarkerProgressData, ISpecterApiResponseData
    {
    }
}