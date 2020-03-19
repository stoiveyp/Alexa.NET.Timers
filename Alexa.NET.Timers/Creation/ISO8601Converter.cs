using System;
using System.Xml;
using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class ISO8601Converter:JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(XmlConvert.ToString((TimeSpan)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return XmlConvert.ToTimeSpan(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan).IsAssignableFrom(objectType);
        }
    }
}