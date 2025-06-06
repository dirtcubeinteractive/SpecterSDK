using SpecterSDK.API.v2.Account;
using SpecterSDK.API.v2.Achievements;
using SpecterSDK.API.v2.App;
using SpecterSDK.API.v2.Auth;
using SpecterSDK.API.v2.Competitions;
using SpecterSDK.API.v2.Events;
using SpecterSDK.API.v2.Inventory;
using SpecterSDK.API.v2.Leaderboards;
using SpecterSDK.API.v2.LiveOps;
using SpecterSDK.API.v2.Matches;
using SpecterSDK.API.v2.Players;
using SpecterSDK.API.v2.Progression;
using SpecterSDK.API.v2.RealMoneyGaming;
using SpecterSDK.API.v2.Stores;
using SpecterSDK.API.v2.Wallet;

namespace SpecterSDK.Shared.Versions
{
    public class SpecterApiV2 : SpecterApiBase
    {
        public SPAccountApiClientV2 Account { get; private set; }
        public SPAchievementsApiClientV2 Achievements { get; private set; }
        public SPAppApiClientV2 App { get; private set; }
        public SPAuthApiClientV2 Auth { get; private set; }
        public SPCompetitionsApiClientV2 Competitions { get; private set; }
        public SPEventsApiClientV2 Events { get; private set; }
        public SPInventoryApiClientV2 Inventory { get; private set; }
        public SPLeaderboardApiClientV2 Leaderboards { get; private set; }
        public SPLiveOpsApiClientV2 LiveOps { get; private set; }
        public SPMatchesApiClientV2 Matches { get; private set; }
        public SPPlayersApiClientV2 Players { get; private set; }
        public SPProgressionApiClientV2 Progression { get; private set; }
        public SPRmgApiClientV2 Rmg { get; private set; }
        public SPStoresApiClientV2 Stores { get; private set; }
        public SPWalletApiClientV2 Wallet { get; private set; }
        
        public override void Initialize(SpecterRuntimeConfig config)
        {
            Account = new SPAccountApiClientV2(config);
            Achievements = new SPAchievementsApiClientV2(config);
            App = new SPAppApiClientV2(config);
            Auth = new SPAuthApiClientV2(config);
            Competitions = new SPCompetitionsApiClientV2(config);
            Events = new SPEventsApiClientV2(config);
            Inventory = new SPInventoryApiClientV2(config);
            Leaderboards = new SPLeaderboardApiClientV2(config);
            LiveOps = new SPLiveOpsApiClientV2(config);
            Matches = new SPMatchesApiClientV2(config);
            Players = new SPPlayersApiClientV2(config);
            Progression = new SPProgressionApiClientV2(config);
            Rmg = new SPRmgApiClientV2(config);
            Stores = new SPStoresApiClientV2(config);
            Wallet = new SPWalletApiClientV2(config);
        }

        public override void Dispose()
        {
            Account = null;
            Achievements = null;
            App = null;
            Auth = null;
            Competitions = null;
            Events = null;
            Inventory = null;
            Leaderboards = null;
            LiveOps = null;
            Matches = null;
            Players = null;
            Progression = null;
            Rmg = null;
            Stores = null;
            Wallet = null;
        }
    }
}