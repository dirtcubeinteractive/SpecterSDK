using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Store Response Data models
    
    [Serializable]
    public class SPStoreResponseBaseData : SPResourceResponseData { }

    [Serializable]
    public class SPStoreResponseData : SPStoreResponseBaseData, ISpecterMasterData
    {
        public int gamePlatformMasterId { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public int categoriesCount { get; set; }
    }

    [Serializable]
    public class SPStoreResponseDataList : SPResponseDataList<SPStoreResponseData> { }
    
    #endregion

    #region Store Category Response Data models
    
    [Serializable]
    public class SPStoreCategoryResponseData : SPStoreResponseBaseData, ISpecterApiResponseData
    {
        public int contentsCount { get; set; }
    }
    [Serializable]
    public class SPStoreCategoryResponseDataList : SPResponseDataList<SPStoreCategoryResponseData> { }

    #endregion

    #region Store Category Content Response Data models
    
    [Serializable]
    public class SPStoreCategoryContentResponseData : ISpecterApiResponseData
    {
        public List<SPStoreItemResponseData> items { get; set; }
        public List<SPStoreBundleResponseData> bundles { get; set; }
        public List<SPStoreCurrencyResponseData> currencies { get; set; }
    }
    
    #endregion

    #region Store Purchased Response Data Model
    
    [Serializable]
    public class SPPurchaseResponseBaseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items { get; set; }
        public List<SPInventoryBundleResponseData> bundles { get; set; }
    }

    [Serializable]
    public class SPCustomPurchaseResponseData : SPPurchaseResponseBaseData { }

    [Serializable]
    public class SPDefaultPurchaseResponseData : SPPurchaseResponseBaseData { }

    #endregion
}