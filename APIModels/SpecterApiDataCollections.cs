using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels
{
    [Serializable]
    public class SPResponseDataList<T> : List<T>, ISpecterApiResponseData where T : ISpecterApiResponseData { }
    
    [Serializable]
    public class SPResponseDataDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISpecterApiResponseData { }
}