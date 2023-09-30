using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using UnityEngine;

namespace SpecterSDK.ObjectModels
{
    #region Currencies

    public abstract class SpecterCurrencyBase : SpecterResource
    {
        public string Code;
    }

    public class SpecterCurrency : SpecterCurrencyBase, ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterCurrency() { }
        public SpecterCurrency(SPCurrencyResponseData data)
        {
            
        }
    }

    public class SpecterWalletCurrency : SpecterCurrencyBase
    {
        public float Balance;

        public SpecterWalletCurrency() { }
        public SpecterWalletCurrency(SPWalletCurrencyResponseData data)
        {
            
        }
    }

    #endregion

    #region Items

    public abstract class SpecterItemBase : SpecterResource
    {
        
    }

    public class SpecterItem : SpecterItemBase, ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterItem() {}
        public SpecterItem(SPItemResponseData data)
        {
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterInventoryItem : SpecterItemBase
    {
        
    }

    #endregion
}