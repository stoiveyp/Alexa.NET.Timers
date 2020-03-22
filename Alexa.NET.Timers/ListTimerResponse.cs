using Newtonsoft.Json;

namespace Alexa.NET.Timers
{
    public class ListTimerResponse
    {
        [JsonProperty("timers")]
        public TimerResponse[] Timers { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("nextToken", NullValueHandling = NullValueHandling.Ignore)]
        public string NextToken { get; set; }
    }
}
