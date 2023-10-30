using System;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared
{
    [Serializable]
    public class SPPriceTypes : SPEnum<SPPriceTypes>
    {
        public static readonly SPPriceTypes RMG = new SPPriceTypes(0, nameof(RMG), nameof(RMG));
        public static readonly SPPriceTypes VirtualCurrency = new SPPriceTypes(1, "virtual currency", nameof(VirtualCurrency));
        public static readonly SPPriceTypes IAP = new SPPriceTypes(2, nameof(IAP), nameof(IAP));
        
        public SPPriceTypes(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
}