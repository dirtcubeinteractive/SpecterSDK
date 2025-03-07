namespace SpecterSDK.Shared.Versions
{
    public abstract class SpecterApiBase
    {
        public abstract void Initialize(SpecterRuntimeConfig config);
        public abstract void Dispose();
    }
}