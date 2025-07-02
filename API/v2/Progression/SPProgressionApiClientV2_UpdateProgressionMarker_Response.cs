using System;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Progression
{
    [Serializable]
    public class SPUpdateProgressionMarkerResponse : SPMarkerProgressData, ISpecterApiResponseData
    {
    }
}