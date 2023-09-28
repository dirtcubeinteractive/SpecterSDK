namespace SpecterSDK.ObjectModels
{
    using Interfaces;
    using APIDataModels.Interfaces;
    public abstract class SPObjectBase<TSpecterObject, TData> : ISpecterObject
        where TSpecterObject : SPObjectBase<TSpecterObject, TData>, new()
        where TData : class, ISpecterApiResponseData, new()
    {
        public static TSpecterObject CreateFromData(TData data)
        {
            var specterObject = new TSpecterObject();
            specterObject.Map(data);
            return specterObject;
        }

        public abstract void Map(TData data);
    }
}