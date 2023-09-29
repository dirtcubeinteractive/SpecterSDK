using System;
using System.Linq;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared
{
    public enum SPJsonFormatting
    {
        None = Formatting.None,
        Indented = Formatting.Indented
    }
    
    public class SpecterJson
    {
        private static JsonSerializerSettings s_SerializerSettings;

        private static JsonSerializerSettings Settings
        {
            get
            {
                if (s_SerializerSettings == null)
                {
                    s_SerializerSettings = new JsonSerializerSettings();
                    s_SerializerSettings.Converters.Add(new SPEnumJsonConverter<SPRewardClaimType>());
                    s_SerializerSettings.Converters.Add(new SPEnumJsonConverter<SPTaskType>());
                }

                return s_SerializerSettings;
            }
        }

        public static string SerializeObject(object value, SPJsonFormatting formatting = SPJsonFormatting.None)
        {
            return JsonConvert.SerializeObject(value, (Formatting)formatting, Settings);
        }

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }
        
        public static string ToQueryString(object obj, string prefix = null)
        {
            if (obj == null)
                return string.Empty;

            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select ToQueryStringProperty(p.Name, p.GetValue(obj, null), prefix);

            return string.Join("&", properties.Where(p => !string.IsNullOrEmpty(p)));
        }

        protected static string ToQueryStringProperty(string name, object value, string prefix)
        {
            string key = string.IsNullOrEmpty(prefix) ? Uri.EscapeDataString(name) : $"{prefix}.{Uri.EscapeDataString(name)}";

            if (value is string or ValueType) // Check if it's a value type or string
            {
                return $"{key}={Uri.EscapeDataString(value.ToString())}";
            }

            // If nested object, recursively call to serialize
            return ToQueryString(value, key);
        }

        public static void AddConverter(JsonConverter converter)
        {
            Settings.Converters.Add(converter);
        }

        public static void RemoveConverter(JsonConverter converter)
        {
            Settings.Converters.Remove(converter);
        }
    }
}
