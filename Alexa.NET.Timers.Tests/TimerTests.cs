using System;
using Alexa.NET.ConnectionTasks.Inputs;
using Alexa.NET.Request.Type;
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
            var request = new CreateTimerRequest(
                TimeSpan.FromSeconds(30),
                new LaunchTaskOperation(
                    new LaunchRequestTask
                    {
                        Name = "<customTask.NAME>",
                        Version="<customTask.VERSION>",
                        Input = new PrintImageV1
                        {
                            Title = "Beautiful scenic image",
                            Description = "test",
                            Url = "http://www.example.com/beautiful-scenic-image.jpg",
                            ImageV1Type = PrintImageV1Type.JPEG
                        }
                    },
                    new LocaleText
                    {
                        Locale = "en-US",
                        Text = "Your print timer is up! Would you like to pass focus back skill {continueWithSkillName}"
                    }), 
                DisplayVisibility.Hidden,
                true,
                "print");
            Assert.True(Utility.CompareJson(request, "CreateTimerLaunchTask.json"));
        }

        [Fact]
        public void CreateRequestNotifyOnly()
        {
            var request = new CreateTimerRequest(
                TimeSpan.FromMinutes(10),
                new NotifyOnlyOperation(),
                DisplayVisibility.Hidden,
                true,
                "exercise");
            Assert.True(Utility.CompareJson(request,"CreateTimerNotifyOnly.json"));
        }
    }
}
