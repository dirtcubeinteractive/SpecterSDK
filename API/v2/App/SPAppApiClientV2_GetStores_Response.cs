using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.App
{
   [Serializable]
   public class SPGetStoresResponse : ISpecterMasterResponse
   {
      public List<SPStoreData> stores { get; set; }
      public int totalCount { get; set; }
      public DateTime? lastUpdate { get; set; }
   }

   [Serializable]
   public class SPStoreData : ISpecterResourceData, ISpecterMasterData, ISpecterUnlockableData
   {
      public string uuid { get; set; }
      public string id { get; set; }
      public string name { get; set; }
      public string description { get; set; }
      public string iconUrl { get; set; }
      
      public List<SPPlatformBaseData> platforms { get; set; }
      public List<SPLocationData> locations { get; set; }
      
      public SPUnlockConditionsData unlockConditions { get; set; }
      
      public List<string> tags { get; set; }
      public Dictionary<string, object> meta { get; set; }
   }
}