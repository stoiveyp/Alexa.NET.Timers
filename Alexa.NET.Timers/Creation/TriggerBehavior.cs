using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class TriggerBehavior
    {
        [JsonProperty("operation")]
        public Operation Operation { get; set; }
    }
}