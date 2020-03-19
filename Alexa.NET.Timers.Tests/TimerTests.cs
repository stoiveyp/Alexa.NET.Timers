using System;
using Xunit;

namespace Alexa.NET.Timers.Tests
{
    public class TimerTests
    {
        [Fact]
        public void CreateRequestAnnounce()
        {
            var request = new CreateTimerRequest(
                TimeSpan.FromMinutes(10),
                new AnnounceOperation(new LocaleText
                {
                    Locale = "en-US",
                    Text = "That's enough stretching, start to run"
                }),
                DisplayVisibility.Visible,
                true,
                "exercise"
                );
            Assert.True(Utility.CompareJson(request,"CreateTimerAnnounce.json"));
        }

        [Fact]
        public void CreateRequestLaunchTask()
        {
            Assert.True(false);
        }

        [Fact]
        public void CreateRequestNotifyOnly()
        {
            Assert.True(false);
        }
    }
}
