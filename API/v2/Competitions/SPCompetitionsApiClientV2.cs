using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Competitions
{
    public sealed class SPCompetitionRankingAttribute : SPEnum<SPCompetitionRankingAttribute>
    {
        public static readonly SPCompetitionRankingAttribute Rankings = new SPCompetitionRankingAttribute(1, "rankings", nameof(Rankings));
        public static readonly SPCompetitionRankingAttribute TotalRanks = new SPCompetitionRankingAttribute(2, "totalRanks", nameof(TotalRanks));
        
        public SPCompetitionRankingAttribute(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }
    
    public partial class SPCompetitionsApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPCompetitionsApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}