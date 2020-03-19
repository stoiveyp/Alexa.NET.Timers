using System;
using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class CreateTimerRequest
    {
        public CreateTimerRequest() { }

        public CreateTimerRequest(TimeSpan duration, Operation operation, DisplayVisibility displayVisibility, bool playAudible, string label = null)
        {
            Duration = duration;
            CreationBehavior = new CreationBehavior(displayVisibility);
            TriggeringBehavior = new TriggerBehavior(operation,playAudible);
            Label = label;
        }

        [JsonProperty("duration"),JsonConverter(typeof(ISO8601Converter))]
        public TimeSpan Duration { get; set; }

        [JsonProperty("timerLabel",NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("creationBehavior")]
        public CreationBehavior CreationBehavior { get; set; }

        [JsonProperty("triggeringBehavior")]
        public TriggerBehavior TriggeringBehavior { get; set; }
    }
}