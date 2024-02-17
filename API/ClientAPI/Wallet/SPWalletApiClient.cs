using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Wallet
{
    /// <summary>
    /// <para>
    /// The SPWalletApiClient class provides methods for managing in-game currencies and their respective balances for a user. 
    /// This is part of Specter's Wallet API, which equips developers with endpoints to add, retrieve,
    /// update balances from a user's currency wallets.
    /// </para>
    /// <para>
    /// The enhancement of in-game experiences through seamless interaction with in-game currencies is one of
    /// the key benefits of using the Wallet API.
    /// </para>
    /// <para>
    /// See the Wallet section in the <a href="https://doc.specterapp.xyz">Specter API Documentation</a> for more info.
    /// </para>
    /// <remarks>
    /// Wallets on Specter are NOT cryptocurrency wallets. The term <b>wallet</b> is simply used to represent each currency within your app
    /// and their respective balances for a user/player of your game/app. Think of wallets like a currency equivalent to an item inventory.
    /// </remarks>
    /// </summary>
    public partial class SPWalletApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Wallet API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPWalletApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}