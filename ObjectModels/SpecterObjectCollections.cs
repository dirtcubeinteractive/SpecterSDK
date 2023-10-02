using System.Collections.Generic;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterObjectList<T> : List<T>, ISpecterObject { }
    
    public class SpecterObjectSet<T> : HashSet<T>, ISpecterObject { }

    public class SpecterObjectDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISpecterObject where TVal : class, ISpecterObject { }
}