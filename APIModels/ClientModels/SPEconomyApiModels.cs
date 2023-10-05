using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Response Data Models

    // Base for currency data in SDK responses
    [Serializable]
    public abstract class SPCurrencyResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string iconUrl { get; set; }
        public string type { get; set; }

    }
    
    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData, ISpecterMasterData
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

    public class SPWalletCurrencyResponseDataList : SPResponseDataList<SPWalletCurrencyResponseData>
    {

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
    public class SPItemResponseData : SPItemResponseBaseData, ISpecterMasterData
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