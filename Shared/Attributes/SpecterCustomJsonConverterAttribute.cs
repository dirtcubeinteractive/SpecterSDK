using System;

namespace SpecterSDK.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SpecterCustomJsonConverterAttribute : Attribute
    {
        public SpecterCustomJsonConverterAttribute() { }
    }
}