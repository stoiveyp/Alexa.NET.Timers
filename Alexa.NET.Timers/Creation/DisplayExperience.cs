using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers
{
    public class DisplayExperience
    {
        public DisplayExperience() { }

        public DisplayExperience(DisplayVisibility visibility)
        {
            Visibility = visibility;
        }

        [JsonProperty("visibility"), JsonConverter(typeof(StringEnumConverter))]
        public DisplayVisibility Visibility { get; set; }

    }
}