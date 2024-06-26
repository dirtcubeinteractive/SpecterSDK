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
        public List<SPLocationData> storeLocations { get; set; }
        public List<SPPlatformBaseData> storePlatforms { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public int categoriesCount { get; set; }
    }

    [Serializable]
    public class SPGetStoresResponseData : ISpecterApiResponseData
    {
        public List<SPStoreResponseData> stores { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }
    
    #endregion

    #region Store Category Response Data models
    
    [Serializable]
    public class SPStoreCategoryResponseData : SPStoreResponseBaseData
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

        public int totalItemsCount { get; set; }

        public int totalBundlesCount { get; set; }
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