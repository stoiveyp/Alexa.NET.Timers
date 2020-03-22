namespace Alexa.NET.Timers.Creation
{
    public class NotifyOnlyOperation : Operation
    {
        public const OperationType TypeValue = OperationType.NotifyOnly;
        public override OperationType Type => TypeValue;
    }
}