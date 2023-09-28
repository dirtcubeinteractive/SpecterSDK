namespace SpecterSDK.ObjectModels
{
    using Interfaces;
    using APIDataModels.Interfaces;

    public abstract class SPObjectBase<TData> : ISpecterObject
        where TData : class, ISpecterApiResponseData, new()
    {
        public static TSpectorObject Create<TSpectorObject>(TData data)
            where TSpectorObject : SPObjectBase<TData>, new()
        {
            var specterObject = new TSpectorObject();
            specterObject.Map(data);
            return specterObject;
        }

        public abstract void Map(TData data);
    }
}