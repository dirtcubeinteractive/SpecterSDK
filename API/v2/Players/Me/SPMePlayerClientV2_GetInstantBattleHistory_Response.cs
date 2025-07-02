using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyInstantBattleHistoryResponse : List<SPInstantBattleHistoryEntryData>, ISpecterApiResponseData { }
}