using System;
using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.Shared;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.Matches
{
    /// <summary>
    /// Represents the base required user information within a match session.
    /// </summary>
    [Serializable]
    public class SPMatchUserInfo
    {
        /// <summary>
        /// ID of the user participating in the match
        /// </summary>
        [JsonRequired]
        public string id;
    }

    /// <summary>
    /// Represents the additional user information at the end of a match session with added outcome and parameters data.
    /// <remarks>
    /// <para>
    /// This class also optionally holds any custom or Specter parameters you wish to send to Specter with the <see cref="SPMatchesApiClient.EndMatchSessionAsync"/>> API call.
    /// </para>
    /// <para>
    /// See the <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/events/event-parameters">Specter User Manual</a> for more information about events and parameters.
    /// </para>
    /// </remarks>
    /// </summary>
    [Serializable]
    public class SPEndMatchSessionUserInfo : SPMatchUserInfo, ISpecterEventConfigurable
    {
        /// <summary>
        /// Outcome of the user at the end of the match session.
        /// <remarks>
        /// The outcome value of the match can vary based on the rules for your match.
        /// </remarks>
        /// <example>
        /// If your match is a highest score match, it would simply be the user's score.
        /// If your match is a time trial, it would be the time in milliseconds.
        /// If your match is position based, then it would be the finish position of the user, and so on.
        /// </example>
        /// </summary>
        public int outcome;
        
        /// <summary>
        /// Any custom parameters you wish to send.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
        
        /// <summary>
        /// Any Specter parameters you wish to send.
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
    }
    
    /// <summary>
    /// Represents the base request structure for operations related to match sessions.
    /// </summary>
    [Serializable]
    public abstract class SPMatchSessionRequestBase : SPApiRequestBase
    {
        /// <summary>
        /// The ID of the match
        /// </summary>
        public string matchId;
        
        /// <summary>
        /// The competition ID if you wish to start a session for a user for a competition the match
        /// is associated with. This is an optional field.
        /// </summary>
        public string competitionId;
        
        /// <summary>
        /// User information within a match session.
        /// </summary>
        public List<SPMatchUserInfo> userInfo;
    }
    
    /// <summary>
    /// The <see cref="SPMatchesApiClient"/> provides a comprehensive toolset for managing match sessions, a core component within the Specter API ecosystem. 
    /// This class is integral for facilitating competitive gaming and matchmaking operations in your Specter-powered applications.
    /// <para>
    /// Key functionalities include:
    /// <list type="bullet">
    /// <item>Creating a match session</item>
    /// <item>Starting a match session</item>
    /// <item>Ending a match session with users' match outcomes</item>
    /// </list>
    /// See the Matches section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a> for full info.
    /// </para>
    /// By leveraging these interfaces, developers can orchestrate complex competitive structures ranging from small-scale skirmishes to large, tournament-style events.
    /// </summary>
    public partial class SPMatchesApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        public SPMatchesApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}