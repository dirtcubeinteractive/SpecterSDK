using System;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.LiveOps
{
    [Serializable]
    public class SPGetServerTimeResponse : SPServerTimeData, ISpecterApiResponseData { }
}