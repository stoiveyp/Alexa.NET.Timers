using Newtonsoft.Json;

namespace Alexa.NET.Timers.Creation
{
    public class LocaleText
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}