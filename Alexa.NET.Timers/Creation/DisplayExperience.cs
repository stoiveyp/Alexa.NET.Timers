using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class DisplayExperience
    {
        [JsonProperty("visibility")]
        public DisplayVisibility Visibility { get; set; }

    }
}