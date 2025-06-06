using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.Shared.Networking.Models
{
    /// <summary>
    /// A list of Specter data classes. Used when the response is expected to return a list.
    /// </summary>
    /// <typeparam name="T">Type of Specter data contained in the list</typeparam>
    [Serializable]
    public class SPResponseDataList<T> : List<T>, ISpecterApiResponseData where T : class, ISpecterApiResponseData, new() { }
    
    /// <summary>
    /// A dictionary of Specter data classes.
    /// </summary>
    /// <typeparam name="TKey">Type of key. Typically 'string'</typeparam>
    /// <typeparam name="TVal">Type of value - a subclass of <see cref="ISpecterApiResponseData"/></typeparam>
    [Serializable]
    public class SPResponseDataDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISpecterApiResponseData where TVal : class, ISpecterApiResponseData, new() { }
}