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
    public sealed class SPAppPlatform : SPEnum<SPAppPlatform>
    {
        public static readonly SPAppPlatform Android = new SPAppPlatform(1, nameof(Android).ToLower(), nameof(Android));
        public static readonly SPAppPlatform iOS = new SPAppPlatform(2, nameof(iOS).ToLower(), nameof(iOS));
        public static readonly SPAppPlatform Windows = new SPAppPlatform(3, "Windows (PC) (Steam, Epic Games Store, Microsoft Store)", nameof(Windows));
        public static readonly SPAppPlatform macOS = new SPAppPlatform(4, "macOS (PC) (Steam, Mac App Store, Epic Games Store)", nameof(macOS));
        public static readonly SPAppPlatform Linux = new SPAppPlatform(5, "Linux (PC) (Steam, other Linux distributions)", nameof(Linux));
        public static readonly SPAppPlatform PlayStation_4 = new SPAppPlatform(6, "PlayStation 4 (PS4)", nameof(PlayStation_4).Replace("_", " "));
        public static readonly SPAppPlatform PlayStation_5 = new SPAppPlatform(7, "PlayStation 5 (PS5)", nameof(PlayStation_5).Replace("_", " "));
        public static readonly SPAppPlatform Xbox_One = new SPAppPlatform(8, "Xbox One", nameof(Xbox_One).Replace("_", " "));
        public static readonly SPAppPlatform Xbox_Series_XS = new SPAppPlatform(9, "Xbox Series X|S", nameof(Xbox_Series_XS).Replace("_", " "));
        public static readonly SPAppPlatform Nintendo_Switch = new SPAppPlatform(10, "Nintendo Switch", nameof(Nintendo_Switch).Replace("_", " "));
        public static readonly SPAppPlatform Meta_Quest = new SPAppPlatform(11, "Meta Quest (formerly Oculus Quest)", nameof(Meta_Quest).Replace("_", " "));
        public static readonly SPAppPlatform Meta_Quest_2 = new SPAppPlatform(12, "Meta Quest 2 (formerly Oculus Quest 2)", nameof(Meta_Quest_2).Replace("_", " "));
        public static readonly SPAppPlatform Meta_Quest_Pro = new SPAppPlatform(13, "Meta Quest Pro", nameof(Meta_Quest_Pro).Replace("_", " "));
        public static readonly SPAppPlatform HTC_Vive = new SPAppPlatform(14, "HTC Vive (VR)", nameof(HTC_Vive).Replace("_", " "));
        public static readonly SPAppPlatform Valve_Index = new SPAppPlatform(15, "Valve Index (VR)", nameof(Valve_Index).Replace("_", " "));
        public static readonly SPAppPlatform Google_Stadia = new SPAppPlatform(16, "Google Stadia (Cloud)", nameof(Google_Stadia).Replace("_", " "));
        public static readonly SPAppPlatform Amazon_Luna = new SPAppPlatform(17, "Amazon Luna (Cloud)", nameof(Amazon_Luna).Replace("_", " "));
        public static readonly SPAppPlatform Nvidia_GeForce_Now = new SPAppPlatform(18, "NVIDIA GeForce Now (Cloud)", nameof(Nvidia_GeForce_Now).Replace("_", " "));
        public static readonly SPAppPlatform Microsoft_xCloud = new SPAppPlatform(19, "Microsoft xCloud (Cloud)", nameof(Microsoft_xCloud).Replace("_", " "));
        public static readonly SPAppPlatform WatchOS = new SPAppPlatform(20, "WatchOS (Wearables)", nameof(WatchOS).Replace("_", " "));
        public static readonly SPAppPlatform WearOS = new SPAppPlatform(21, "Wear OS (Google) (Wearables)", nameof(WearOS).Replace("_", " "));
        public static readonly SPAppPlatform Apple_TV = new SPAppPlatform(22, "Apple TV (tvOS)", nameof(Apple_TV).Replace("_", " "));
        public static readonly SPAppPlatform Android_TV = new SPAppPlatform(23, "Android TV (Google TV)", nameof(Android_TV).Replace("_", " "));
        public static readonly SPAppPlatform AmazonFire_TV = new SPAppPlatform(24, "Amazon Fire TV", nameof(AmazonFire_TV).Replace("_", " "));
        public static readonly SPAppPlatform Roku = new SPAppPlatform(25, nameof(Roku), nameof(Roku));
        public static readonly SPAppPlatform VisionOS = new SPAppPlatform(26, nameof(VisionOS), nameof(VisionOS));
        public static readonly SPAppPlatform WebGL = new SPAppPlatform(27, nameof(WebGL), nameof(WebGL));
        

        public SPAppPlatform(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }

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

    [Serializable]
    public sealed class SPRarity : SPEnum<SPRarity>
    {
        public static readonly SPRarity Common = new SPRarity(1, nameof(Common).ToLower(), nameof(Common));
        public static readonly SPRarity Uncommon = new SPRarity(2, nameof(Uncommon).ToLower(), nameof(Uncommon));
        public static readonly SPRarity Rare = new SPRarity(3, nameof(Rare).ToLower(), nameof(Rare));
        public static readonly SPRarity Epic = new SPRarity(4, nameof(Epic).ToLower(), nameof(Epic));
        public static readonly SPRarity Legendary = new SPRarity(5, nameof(Legendary).ToLower(), nameof(Legendary));
        
        public SPRarity(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }
    
    /// <summary>
    /// Represents the currency types available in the system.
    /// </summary>
    [Serializable]
    public sealed class SPCurrencyTypeV2 : SPEnum<SPCurrencyTypeV2>
    {
        /// <summary>
        /// Type used for virtual currencies and Real Money Gaming currencies.
        /// </summary>
        public static readonly SPCurrencyTypeV2 Virtual = new SPCurrencyTypeV2(0, "virtual", "Virtual");
        
        /// <summary>
        /// Type used for IAP currencies.
        /// </summary>
        public static readonly SPCurrencyTypeV2 Real = new SPCurrencyTypeV2(1, "real", "Real");
        
        private SPCurrencyTypeV2(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    #endregion

    #region Events

    [Serializable]
    public sealed class SPStatCollectionMode : SPEnum<SPStatCollectionMode>
    {
        public static readonly SPStatCollectionMode Cumulative = new SPStatCollectionMode(0, nameof(Cumulative).ToLower(), nameof(Cumulative));
        public static readonly SPStatCollectionMode One_Shot = new SPStatCollectionMode(1, nameof(One_Shot).Replace("_", "-").ToLower(), nameof(One_Shot).Replace("_", " "));
        
        public SPStatCollectionMode(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }

    [Serializable]
    public sealed class SPParamDataType : SPEnum<SPParamDataType>
    {
        public static readonly SPParamDataType String = new SPParamDataType(1, nameof(String).ToLower(), nameof(String));
        public static readonly SPParamDataType Integer = new SPParamDataType(2, nameof(Integer).ToLower(), nameof(Integer));
        public static readonly SPParamDataType Boolean = new SPParamDataType(3, nameof(Boolean).ToLower(), nameof(Boolean));
        public static readonly SPParameterType Float = new SPParameterType(4, nameof(Float).ToLower(), nameof(Float));
        
        public SPParamDataType(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }
    
    [Serializable]
    public sealed class SPParameterType : SPEnum<SPParameterType>
    {
        public static readonly SPParameterType State = new SPParameterType(0, nameof(State).ToLower(), nameof(State));
        public static readonly SPParameterType Statistic = new SPParameterType(1, nameof(Statistic).ToLower(), nameof(Statistic));
        
        public SPParameterType(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
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