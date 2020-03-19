using System;
using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class CreateTimerRequest
    {
        [JsonProperty("duration"),JsonConverter(typeof(ISO8601Converter))]
        public TimeSpan Duration { get; set; }

        [JsonProperty("timerLabel",NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("creationBehavior")]
        public CreationBehavior CreationBehavior { get; set; }

        [JsonProperty("triggerBehavior")]
        public TriggerBehavior TriggerBehavior { get; set; }

        [JsonProperty("notificationConfig")]
        public NotificationConfig NotificationConfig { get; set; }
    }
}