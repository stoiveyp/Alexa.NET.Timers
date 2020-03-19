using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class LaunchTaskOperation : Operation
    {
        public const OperationType TypeValue = OperationType.LaunchTask;
        public override OperationType Type => TypeValue;

        [JsonProperty("textToConfirm")]
        public LocaleText TextToConfirm { get; set; }

        [JsonProperty("task")]
        public LaunchRequestTask Task { get; set; }
    }
}