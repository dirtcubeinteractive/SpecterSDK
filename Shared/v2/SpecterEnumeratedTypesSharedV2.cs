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
        public static readonly SPAccountAuthProvider CustomId = new SPAccountAuthProvider(2, "customId", "Custom ID");
        
        private SPAccountAuthProvider(int id, string name, string displayName) : base(id, name, displayName) { }
    }

    #endregion

    #region App

    [Serializable]
    public sealed class SPRankingMethod : SPEnum<SPRankingMethod>
    {
        public static readonly SPRankingMethod High_Score = new SPRankingMethod(1, nameof(High_Score).ToLower(), nameof(High_Score).Replace("_", " "));
        public static readonly SPRankingMethod Low_Score = new SPRankingMethod(2, "time_trial", nameof(Low_Score).Replace("_", " "));
        public static readonly SPRankingMethod Position_Weighting = new SPRankingMethod(4, nameof(Position_Weighting).ToLower(), nameof(Position_Weighting).Replace("_", " "));
        public static readonly SPRankingMethod Cumulative_Score = new SPRankingMethod(5, nameof(Cumulative_Score).ToLower(), nameof(Cumulative_Score).Replace("_", " "));
        
        private SPRankingMethod(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }

    #endregion

    #region Economy
    
    /// <summary>
    /// Represents the currency types available in the system.
    /// </summary>
    [Serializable]
    public sealed class SPCurrencyTypeV2 : SPEnum<SPCurrencyTypeV2>
    {
        public static readonly SPCurrencyTypeV2 Virtual = new SPCurrencyTypeV2(0, "virtual", "Virtual");
        public static readonly SPCurrencyTypeV2 Real = new SPCurrencyTypeV2(1, "real", "Real");
        
        private SPCurrencyTypeV2(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    #endregion

    #region LiveOps

    /// <summary>
    /// Represents the different units used for intervals in LiveOps scheduling.
    /// </summary>
    [Serializable]
    public sealed class SPLiveOpsInterval : SPEnum<SPLiveOpsInterval>
    {
        public static readonly SPLiveOpsInterval Minutes = new SPLiveOpsInterval(7, "minutes", "Minutes");
        public static readonly SPLiveOpsInterval Hours = new SPLiveOpsInterval(8, "hours", "Hours");
        public static readonly SPLiveOpsInterval Days = new SPLiveOpsInterval(1, "days", "Days");
        public static readonly SPLiveOpsInterval Weeks = new SPLiveOpsInterval(2, "weeks", "Weeks");
        
        /// <summary>
        /// Custom interval unit is used for 'non-recurring' entities where only the start date and end date are relevant.
        /// </summary>
        public static readonly SPLiveOpsInterval Custom = new SPLiveOpsInterval(6, "custom", "Custom");
        
        /// <summary>
        /// All Time will be deprecated in the future. Please refrain from using it.
        /// </summary>
        public static readonly SPLiveOpsInterval AllTime = new SPLiveOpsInterval(5, "all_time", "All Time");
        
        public SPLiveOpsInterval(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
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

    #region Wallet

    /// <summary>
    /// Represents the different wallet types - mainly relevant if using Real Money Gaming features
    /// </summary>
    [Serializable]
    public sealed class SPWalletType : SPEnum<SPWalletType>
    {
        public static readonly SPWalletType Virtual = new SPWalletType(0, "virtual", nameof(Virtual));
        public static readonly SPWalletType RM_Deposit = new SPWalletType(1, nameof(RM_Deposit).ToLower(), nameof(RM_Deposit));
        public static readonly SPWalletType RM_Winning = new SPWalletType(2, nameof(RM_Winning).ToLower(), nameof(RM_Winning));
        public static readonly SPWalletType RM_Bonus = new SPWalletType(3, nameof(RM_Bonus).ToLower(), nameof(RM_Bonus));
        
        public SPWalletType(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }

    #endregion
}