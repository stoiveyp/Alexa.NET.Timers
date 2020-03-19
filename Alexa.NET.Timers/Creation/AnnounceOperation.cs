namespace Alexa.NET.Timers
{
    public class AnnounceOperation : Operation
    {
        public const OperationType TypeValue = OperationType.Announce;
        public override OperationType Type => TypeValue;

        public LocaleText[] TextToAnnounce { get; set; }
    }
}