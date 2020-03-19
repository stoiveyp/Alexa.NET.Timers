using System.Runtime.Serialization;

namespace Alexa.NET.Timers.Creation
{
    public enum OperationType
    {
        [EnumMember(Value="NOTIFY_ONLY")]
        NotifyOnly,
        [EnumMember(Value = "ANNOUNCE")]
        Announce,
        [EnumMember(Value = "LAUNCH_TASK")]
        LaunchTask
    }
}