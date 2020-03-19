using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers.Creation
{
    public class CreateTimerResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("duration"),JsonConverter(typeof(ISO8601Converter))]
        public TimeSpan Duration { get; set; }
        [JsonProperty("status"),JsonConverter(typeof(StringEnumConverter))]
        public TimerStatus Status { get; set; }
        [JsonProperty("timerLabel")]
        public string Label { get; set; }
        [JsonProperty("triggerTime")]
        public DateTime TriggerTime { get; set; }
        [JsonProperty("createdTime")]
        public DateTime CreatedTime { get; set; }
        [JsonProperty("updatedTime")]
        public DateTime UpdatedTime { get; set; }
        [JsonProperty("remainingTimeWhenPaused"), JsonConverter(typeof(ISO8601Converter))]
        public TimeSpan RemainingTimeWhenPaused { get; set; }
    }
}