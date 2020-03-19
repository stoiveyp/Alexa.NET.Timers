using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class TriggerBehavior
    {
        public TriggerBehavior() { }

        public TriggerBehavior(Operation operation)
        {
            Operation = operation;
        }

        [JsonProperty("operation")]
        public Operation Operation { get; set; }
    }
}