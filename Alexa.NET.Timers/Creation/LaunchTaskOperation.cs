using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace Alexa.NET.Timers.Creation
{
    public class LaunchTaskOperation : Operation
    {
        public const OperationType TypeValue = OperationType.LaunchTask;
        public override OperationType Type => TypeValue;

        public LaunchTaskOperation() { }

        public LaunchTaskOperation(LaunchRequestTask task, params LocaleText[] textToConfirm)
        {
            Task = task;
            TextToConfirm = textToConfirm;
        }

        [JsonProperty("textToConfirm")]
        public LocaleText[] TextToConfirm { get; set; }

        [JsonProperty("task")]
        public LaunchRequestTask Task { get; set; }
    }
}