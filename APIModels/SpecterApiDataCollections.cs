using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels
{
    /// <summary>
    /// A list of Specter data classes. Used when the response is expected to return a list.
    /// </summary>
    /// <typeparam name="T">Type of Specter data contained in the list</typeparam>
    [Serializable]
    public class SPResponseDataList<T> : List<T>, ISpecterApiResponseData where T : class, ISpecterApiResponseData, new() { }
    
    /// <summary>
    /// A dictionary 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    [Serializable]
    public class SPResponseDataDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISpecterApiResponseData where TVal : class, ISpecterApiResponseData, new() { }
}