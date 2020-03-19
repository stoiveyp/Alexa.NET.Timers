using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public abstract class Operation
    {
        [JsonProperty("type")]
        public abstract OperationType Type { get; }
    }
}