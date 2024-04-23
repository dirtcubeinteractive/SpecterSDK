using System;
using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Authentication
{
    /// <summary>
    /// Abstract base class for SPAuthLoginRequestBase.
    /// Represents the base structure for all authentication login requests in the SDK.
    /// </summary>
    [Serializable]
    public abstract class SPAuthLoginRequestBase : SPApiRequestBase, ISpecterEventConfigurable
    {
        /// <summary>
        /// Sets whether to create a new account for the user during login.
        /// </summary>
        /// <remarks>
        /// This property is used in the SPAuthLoginCustomIdRequest class to specify whether a new user account should be created if the user does not exist.
        /// By default, the value is false, meaning that a new account will not be created. If set to true, a new account will be created if the specified user ID does not exist.
        /// </remarks>
        /// <example>
        /// When creating a <see cref="SPAuthLoginCustomIdRequest"/>, if the specified customId does not exist but createAccount is true, a new account will be created for the user and the user will
        /// subsequently be logged in.
        /// </example>
        public bool createAccount { get; set; }
        
        /// <summary>
        /// Dictionary of optional Specter params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
            
        /// <summary>
        /// Dictionary of optional custom params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents the result of an authentication login request.
    /// </summary>
    public class SPAuthLoginResult : SpecterApiResultBase<SPUserAuthResponseData>
    {
        /// <summary>
        /// The profile of the logged in user.
        /// </summary>
        public SpecterUser User { get; private set; }

        /// <summary>
        /// Represents an access token used for authentication.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Represents the entity token returned from an authentication login request.
        /// </summary>
        public string EntityToken { get; private set; }
        
        /// <summary>
        /// Flag that indicates if a new user account was created.
        /// Only applicable if the createAccount flag is set to true in the login request.
        /// See <see cref="SPAuthLoginRequestBase"/> for details.
        /// </summary>
        public bool CreatedAccount { get; private set; }

        protected override void InitSpecterObjectsInternal()
        {
            if (Response.data == null)
                return;
            
            User = new SpecterUser(Response.data.user);
            AccessToken = Response.data.accessToken;
            EntityToken = Response.data.entityToken;
            CreatedAccount = Response.data.createdAccount;
        }
    }

    public partial class SPAuthApiClient: SpecterApiClientBase
    {
        /// <summary>
        /// Authentication APIs do not require any additional authorization except your API key.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAuthApiClient(SpecterRuntimeConfig config) : base(config) {}

        /// <summary>
        /// Stores the authentication context after a successful login request.
        /// </summary>
        /// <param name="result">The result of an authentication login request.</param>
        private void StoreAuthContext(SPAuthLoginResult result)
        {
            if (!result.HasError && !m_Config.UseDebugCredentials)
            {
                m_Config.AccessToken = result.AccessToken;
                m_Config.EntityToken = result.EntityToken;
            }
        }
    }
}
