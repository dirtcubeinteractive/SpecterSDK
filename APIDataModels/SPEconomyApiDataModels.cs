using System;
using System.Collections.Generic;
using SpecterSDK.APIDataModels.Interfaces;

namespace SpecterSDK.APIDataModels
{
    #region Response Data Models

    // Base for currency data in SDK responses
    [Serializable]
    public abstract class SPCurrencyResponseBaseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string code { get; set; }
    }
    
    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData, ISpecterCustomDataObject
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
    
    // User wallet currency data in SDK responses
    [Serializable]
    public class SPWalletCurrencyResponseData : SPCurrencyResponseBaseData
    {
        public float balance { get; set; }
    }
    
    // Base for item data in SDK responses
    [Serializable]
    public abstract class SPItemResponseBaseData
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    // Item master data in SDK responses
    [Serializable]
    public class SPItemResponseData : SPItemResponseBaseData, ISpecterCustomDataObject
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    // Store item data in SDK responses
    [Serializable]
    public class SPStoreItemResponseData : SPItemResponseData
    {
        
    }
    
    // User inventory item data in SDK responses
    [Serializable]
    public class SPInventoryItemResponseData : SPItemResponseBaseData
    {
        
    }

    #endregion
}