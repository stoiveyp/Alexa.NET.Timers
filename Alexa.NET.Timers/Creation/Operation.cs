using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers.Creation
{
    [JsonConverter(typeof(OperationConverter))]
    public abstract class Operation
    {
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public abstract OperationType Type { get; }
    }
}