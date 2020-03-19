using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Timers
{
    public class CreationBehavior
    {
        public CreationBehavior() { }

        public CreationBehavior(DisplayVisibility displayVisibility)
        {
            DisplayExperience = new DisplayExperience(displayVisibility);
        }

        [JsonProperty("displayExperience")]
        public DisplayExperience DisplayExperience { get; set; }
    }
}