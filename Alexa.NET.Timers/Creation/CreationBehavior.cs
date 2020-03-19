using Newtonsoft.Json;

namespace Alexa.NET.Timers.Creation
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