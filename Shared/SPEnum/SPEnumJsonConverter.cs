using System;
using Newtonsoft.Json;

namespace SpecterSDK.Shared.SPEnum
{
    /// <summary>
    /// Provides custom JSON converter for subclasses of SPEnum&lt;T&gt;.
    /// </summary>
    public class SPEnumJsonConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : SPEnum<TEnum>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;
        
        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Name);
        }

        public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
                
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    return GetFromName((string)reader.Value);
                case JsonToken.Integer:
                    return GetFromID(reader.Value!);
                default:
                    throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an SP enum.");
            }
            
            TEnum GetFromName(string name)
            {
                try
                {
                    return SPEnum<TEnum>.FromName(name);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error converting value '{name}' to an SP enum.", ex);
                }
            }

            TEnum GetFromID(object id)
            {
                try
                {
                    var val = (int)Convert.ChangeType(id, typeof(int));
                    return SPEnum<TEnum>.FromValue(val);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error converting value '{id}' to an SP enum.", ex);
                }
            }
        }
    }

    public class SPEnumJsonIntConverter<TEnum> : SPEnumJsonConverter<TEnum>
        where TEnum : SPEnum<TEnum>
    {
        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            if (value is null)
                writer.WriteNull();
            else
                writer.WriteValue(value.Id);
        }
    }
}