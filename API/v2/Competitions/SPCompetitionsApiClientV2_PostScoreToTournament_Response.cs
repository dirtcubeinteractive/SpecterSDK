using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    [Serializable]
    public class SPPostScoreToTournamentResponse : List<object>, ISpecterApiResponseData { }
}