#if NETSTANDARD1_4
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace Jose
{

    public class NewtonsoftMapper : IJsonMapper
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }

        public T Parse<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new NestedCollectionsConverter());
        }
    }

    class NestedCollectionsConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if(typeof(IDictionary<string,object>).IsAssignableFrom(objectType))
            {
                return true;
            }
            if (typeof(IList<object>).IsAssignableFrom(objectType))
            {
                return true;
            }
            if (typeof(IList).IsAssignableFrom(objectType))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(TryReadNext(reader, out object value))
            {
                return value;
            } else
            {
                throw new JsonSerializationException("Unexpected token " + reader.TokenType);
            }
        }

        bool TryReadNext(JsonReader reader, out object value)
        {
            switch(reader.TokenType)
            {
                case JsonToken.StartObject:
                    return TryReadObject(reader, out value);
                case JsonToken.StartArray:
                    return TryReadArray(reader, out value);
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                case JsonToken.Boolean:
                    value = reader.Value;
                    return true;
                case JsonToken.Null:
                    value = null;
                    return true;
                default:
                    value = null;
                    return false;
            }
        }

        bool TryReadObject(JsonReader reader, out object value)
        {
            IDictionary<string,object> map = new Dictionary<string, object>();
            while(reader.Read())
            {
                switch(reader.TokenType)
                {
                    case JsonToken.EndObject:
                        value = map;
                        return true;
                    case JsonToken.PropertyName:
                        string key = reader.Value.ToString();
                        if(!reader.Read())
                        {
                            value = null;
                            return false;
                        }
                        if (TryReadNext(reader, out object t))
                        {
                            map.Add(key, t);
                        }
                        break;
                }
            }
            value = null;
            return false;
        }
        bool TryReadArray(JsonReader reader, out object value)
        {
            IList<object> array = new List<object>();
            while(reader.Read())
            {
                switch(reader.TokenType)
                {
                    case JsonToken.EndArray:
                        value = array;
                        return true;
                    default:
                        if (TryReadNext(reader, out object t))
                        {
                            array.Add(t);
                        }
                        break;
                }
            }
            value = null;
            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
#endif