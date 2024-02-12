using System;

namespace SpecterSDK.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SpecterCustomApiClientAttribute : Attribute
    {
        public string Description { get; private set; }
        
        public SpecterCustomApiClientAttribute() { }

        public SpecterCustomApiClientAttribute(string description)
        {
            Description = description;
        }
    }
}