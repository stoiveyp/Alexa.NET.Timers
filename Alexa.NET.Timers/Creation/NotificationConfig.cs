using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class NotificationConfig
    {
        [JsonProperty("playAudible")]
        public bool PlayAudible { get; set; }
    }
}