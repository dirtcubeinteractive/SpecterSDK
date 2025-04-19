using System;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared.v2
{
    #region Account

    /// <summary>
    /// Represents the identification type for resetting password.
    /// </summary>
    [Serializable]
    public sealed class SPAccountAuthIdType : SPEnum<SPAccountAuthIdType>
    {
        public static readonly SPAccountAuthIdType Email = new SPAccountAuthIdType(0, "email", "Email");
        public static readonly SPAccountAuthIdType Username = new SPAccountAuthIdType(1, "username", "Username");
        
        private SPAccountAuthIdType(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents the account type for account operations.
    /// </summary>
    [Serializable]
    public sealed class SPAccountAuthProvider : SPEnum<SPAccountAuthProvider>
    {
        public static readonly SPAccountAuthProvider Google = new SPAccountAuthProvider(0, "google", "Google");
        public static readonly SPAccountAuthProvider Facebook = new SPAccountAuthProvider(1, "facebook", "Facebook");
        public static readonly SPAccountAuthProvider CustomId = new SPAccountAuthProvider(2, "customId", "Custom ID");
        
        private SPAccountAuthProvider(int id, string name, string displayName) : base(id, name, displayName) { }
    }

    #endregion
    
    #region Rewards

    /// <summary>
    /// Represents the reward type.
    /// </summary>
    [Serializable]
    public sealed class SPRewardType : SPEnum<SPRewardType>
    {
        public static readonly SPRewardType Item = new SPRewardType(0, "item", "Item");
        public static readonly SPRewardType Bundle = new SPRewardType(1, "bundle", "Bundle");
        public static readonly SPRewardType Currency = new SPRewardType(2, "currency", "Currency");
        public static readonly SPRewardType ProgressionMarker = new SPRewardType(3, "progressionMarker", "Progression Marker");
        
        private SPRewardType(int id, string name, string displayName) : base(id, name, displayName) { }
    }

    #endregion
}