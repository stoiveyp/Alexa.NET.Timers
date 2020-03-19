using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers
{
    public abstract class Operation
    {
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public abstract OperationType Type { get; }
    }
}