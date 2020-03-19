using System.Runtime.Serialization;

namespace Alexa.NET.Timers
{
    public enum DisplayVisibility
    {
        [EnumMember(Value="HIDDEN")]
        Hidden,
        [EnumMember(Value="VISIBLE")]
        Visible
    }
}