using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class TriggerBehavior
    {
        public TriggerBehavior() { }

        public TriggerBehavior(Operation operation, bool playAudible)
        {
            Operation = operation;
            NotificationConfig = new NotificationConfig(playAudible);
        }

        [JsonProperty("operation")]
        public Operation Operation { get; set; }

        [JsonProperty("notificationConfig")]
        public NotificationConfig NotificationConfig { get; set; }
    }
}