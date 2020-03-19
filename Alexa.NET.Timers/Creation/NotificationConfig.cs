using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class NotificationConfig
    {
        public NotificationConfig() { }

        public NotificationConfig(bool playAudible)
        {
            PlayAudible = playAudible;
        }

        [JsonProperty("playAudible")]
        public bool PlayAudible { get; set; }
    }
}