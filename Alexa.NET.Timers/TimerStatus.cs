using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Alexa.NET.Timers
{
    public enum TimerStatus
    {
        [EnumMember(Value="ON")]
        On,
        [EnumMember(Value = "OFF")]
        Off,
        [EnumMember(Value = "PAUSED")]
        Paused
    }
}
