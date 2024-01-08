using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Extensions;
using SpecterSDK.Shared.SPEnum;
using UnityEngine;

namespace SpecterSDK.Shared
{
    /// <summary>
    /// Formatting enum mapping Specter Json Formatting options to Newtonsoft.
    /// We use Newtonsoft under the hood
    /// </summary>
    public enum SPJsonFormatting
    {
        None = Formatting.None,
        Indented = Formatting.Indented
    }
    
    /// <summary>
    /// A utility class that provides a consistent way to serialize and deserialize JSON within the Specter SDK,
    /// ensuring that any custom settings or converters are applied uniformly.
    /// </summary>
    public class SpecterJson
    {
        // Centralized settings to ensure consistent JSON operations across the SDK.
        private static JsonSerializerSettings s_SerializerSettings;
        private static JsonSerializerSettings Settings => s_SerializerSettings ??= InitializeSettingsInternal();

        /// <summary>
        /// Serialize an object into a JSON string
        /// </summary>
        /// <param name="value">Object to serialize</param>
        /// <param name="formatting">Format option for the JSON string</param>
        /// <returns></returns>
        public static string SerializeObject(object value, SPJsonFormatting formatting = SPJsonFormatting.None)
        {
            return JsonConvert.SerializeObject(value, (Formatting)formatting, Settings);
        }

        /// <summary>
        /// Deserialize a JSON string into a specified type.
        /// </summary>
        /// <param name="value">JSON string to deserialize</param>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <returns>Deserialized object of type T</returns>
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }

        /// <summary>
        /// Converts an object to a query string format to be appended at the end of a Url
        /// </summary>
        /// <param name="obj">The object to be converted.</param>
        /// <param name="prefix">Prefix for nested properties in the query</param>
        /// <returns>A query string representation of the object.</returns>
        public static string ToQueryString(object obj, string prefix = null)
        {
            if (obj == null)
                return string.Empty;

            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select ToQueryStringProperty(p.Name, p.GetValue(obj, null), prefix);

            return string.Join("&", properties.Where(p => !string.IsNullOrEmpty(p)));
        }

        // Function used in recursive ToQueryString call
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

        /// <summary>
        /// Allows for the addition of custom JsonConverter instances to the default serialization settings.
        /// This can be useful when there's a need to handle specific serialization or deserialization scenarios
        /// that aren't covered by the default settings.
        /// </summary>
        /// <param name="converter">Converter to add</param>
        public static void AddConverter(JsonConverter converter)
        {
            Settings.Converters.Add(converter);
        }

        /// <summary>
        /// Removes a previously added custom JsonConverter from the default serialization settings.
        /// </summary>
        /// <param name="converter">Converter to remove</param>
        public static void RemoveConverter(JsonConverter converter)
        {
            Settings.Converters.Remove(converter);
        }

        /// <summary>
        /// Call Initialize if you do not want Specter Json to lazy init serializer settings.
        /// Else SpecterJson will initialize settings itself on first time use.
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings InitializeSettings()
        {
            return Settings;
        }
        
        // Initialize and return default JSON serializer settings for the Specter SDK
        private static JsonSerializerSettings InitializeSettingsInternal()
        {
            var settings = new JsonSerializerSettings();
            InitializeCustomJsonConverters(settings);
            return settings;
        }
        
        /// <summary>
        /// Initializes and adds any custom JsonConverters that the Specter SDK requires.
        /// This method acts as a central place to manage all custom converters.
        /// </summary>
        /// <param name="settings">Settings to initialize & add converters to</param>
        public static void InitializeCustomJsonConverters(JsonSerializerSettings settings)
        {
            InitializeSpEnumJsonConverters(settings);
        }

        /// <summary>
        /// Discovers and initializes JsonConverters for SPEnum&lt;T&gt; subclasses.
        /// This ensures that any enum-like classes in the SDK are serialized/deserialized correctly.
        /// </summary>
        /// <param name="settings">Settings to initialize and add converters to</param>
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

        // Helper method to add a specific JsonConverter for SPEnum<T> subclasses.
        private static void AddSpEnumJsonConverter<T>(JsonSerializerSettings settings)
        where T : SPEnum<T>
        {
            settings.Converters.Add(new SPEnumJsonConverter<T>());
        }

        public static bool TryConvertObject<T>(object obj, out T result)
        {
            result = default;
            
            if (obj is JObject jObj)
            {
                try
                {
                    result = jObj.ToObject<T>();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to convert JObject to {typeof(T)} with exception: {e.Message}");
                    return false;
                }
            }
            
            if (obj is JArray jArr)
            {
                try
                {
                    result = jArr.ToObject<T>();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to convert JArray to {typeof(T)} with exception: {e.Message}");
                    return false;
                }
            }

            try
            {
                result = (T)System.Convert.ChangeType(obj, typeof(T));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to convert object from {obj.GetType().Name} to {typeof(T)} with exception: {e.Message}");
                return false;
            }

            Debug.LogError($"Object is of type {obj.GetType().Name} and is not a JObject or a JArray. Cannot convert to type {typeof(T)}");
            return false;
        }
    }
}
