using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels
{
    public enum SPRewardType
    {
        ProgressionMarker,
        Currency,
        Item,
        Bundle
    }
    
    public sealed class SPRewardClaimType : SPEnum<SPRewardClaimType>
    {
        public static readonly SPRewardClaimType OnClaim = new SPRewardClaimType(0, "on-claim", nameof(OnClaim));
        public static readonly SPRewardClaimType Automatic = new SPRewardClaimType(1, nameof(Automatic).ToLower(), nameof(Automatic));
        
        private SPRewardClaimType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    public sealed class SPTaskType : SPEnum<SPTaskType>
    {
        public static readonly SPTaskType Static = new SPTaskType(0, nameof(Static).ToLower(), nameof(Static));
        public static readonly SPTaskType Daily = new SPTaskType(1, nameof(Daily).ToLower(), nameof(Daily));
        public static readonly SPTaskType Weekly = new SPTaskType(2, nameof(Weekly).ToLower(), nameof(Weekly));
        
        private SPTaskType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [System.Serializable]
    public class SPTask : SPCustomizableObject
    {
        
    }
}