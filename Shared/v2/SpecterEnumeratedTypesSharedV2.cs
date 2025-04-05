using System;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared.v2
{
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