using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers
{
    public class CreationBehavior
    {
        [JsonProperty("displayExperience"),JsonConverter(typeof(StringEnumConverter))]
        public DisplayExperience DisplayExperience { get; set; }
    }
}