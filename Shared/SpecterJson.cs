using Newtonsoft.Json;

namespace SpecterSDK.Shared
{
    public class SpecterJson
    {
        protected internal static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        protected internal static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
