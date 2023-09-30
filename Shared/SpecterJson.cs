using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Extensions;
using SpecterSDK.Shared.SPEnum;
using UnityEngine;

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
        private static JsonSerializerSettings Settings => s_SerializerSettings ??= InitializeSpecterJsonSerializerSettings();

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

        private static JsonSerializerSettings InitializeSpecterJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            InitializeCustomJsonConverters(settings);
            return settings;
        }
        
        public static void InitializeCustomJsonConverters(JsonSerializerSettings settings)
        {
            InitializeSpEnumJsonConverters(settings);
        }

        public static void InitializeSpEnumJsonConverters(JsonSerializerSettings settings)
        {
            var spEnumSubTypes = 
                from Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()
                from Type type in assembly.GetTypes()
                where type.IsClass && !type.IsAbstract && type.IsSubclassOfRawGeneric(typeof(SPEnum<>))
                select type;

            foreach (var subType in spEnumSubTypes)
            {
                MethodInfo genericMethodInfo = typeof(SpecterJson).GetMethod(nameof(AddSpEnumJsonConverter), BindingFlags.Static | BindingFlags.NonPublic);
                MethodInfo addSpEnumConverterMethod = genericMethodInfo!.MakeGenericMethod(subType);
                addSpEnumConverterMethod.Invoke(null, new object[] { settings });
            }
        }

        private static void AddSpEnumJsonConverter<T>(JsonSerializerSettings settings)
        where T : SPEnum<T>
        {
            settings.Converters.Add(new SPEnumJsonConverter<T>());
        }
    }
}
