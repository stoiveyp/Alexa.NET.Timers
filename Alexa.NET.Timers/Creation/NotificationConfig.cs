using Newtonsoft.Json;

namespace Alexa.NET.Timers.Creation
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