using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alexa.NET.Timers
{
    public class AnnounceOperation : Operation
    {
        public const OperationType TypeValue = OperationType.Announce;
        public override OperationType Type => TypeValue;

        public AnnounceOperation() { }

        public AnnounceOperation(params LocaleText[] textToAnnounce)
        {
            TextToAnnounce = textToAnnounce;
        }

        [JsonProperty("textToAnnounce")]
        public LocaleText[] TextToAnnounce { get; set; }
    }
}