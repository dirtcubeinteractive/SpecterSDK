namespace SpecterSDK.ObjectModels
{
    using Interfaces;
    using APIDataModels.Interfaces;

    public abstract class SPObjectBase<TObject, TData> : ISpecterObject
        where TObject : SPObjectBase<TObject, TData>, new()
        where TData : class, ISpecterApiResponseData, new()
    {
        public static TObject Create(TData data)
        {
            var specterObject = new TObject();
            specterObject.Map(data);
            return specterObject;
        }

        public abstract void Map(TData data);
    }
}