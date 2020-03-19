using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers.Creation
{
    public abstract class Operation
    {
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public abstract OperationType Type { get; }
    }
}