using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.ConnectionTasks.Inputs;
using Alexa.NET.Request.Type;
using Alexa.NET.Timers.Creation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Assert.True(Utility.CompareJson(request, "CreateTimerAnnounce.json"));
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
                        Version = "<customTask.VERSION>",
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
            Assert.True(Utility.CompareJson(request, "CreateTimerNotifyOnly.json"));
        }

        [Fact]
        public void CreateResponse()
        {
            var response = new TimerResponse
            {
                Id = "opaque, unique string",
                Duration = TimeSpan.FromMinutes(10),
                Status = TimerStatus.Paused,
                Label = "chicken",
                TriggerTime = DateTime.Parse("2019-09-12T19:10:00.083Z").ToUniversalTime(),
                CreatedTime = DateTime.Parse("2019-09-12T19:00:00.083Z").ToUniversalTime(),
                UpdatedTime = DateTime.Parse("2019-09-12T19:04:35.083Z").ToUniversalTime(),
                RemainingTimeWhenPaused = TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(25))
            };
            Assert.True(Utility.CompareJson(response, "CreateTimerResponse.json"));
        }

        [Fact]
        public async Task CreateCall()
        {
            var http = new HttpClient(new ActionHandler(async req =>
            {
                Assert.Equal("Bearer",req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123",req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/", req.RequestUri.ToString());
                var rawBody = await req.Content.ReadAsStringAsync();
                var bodyObject = JsonConvert.DeserializeObject<CreateTimerRequest>(rawBody);
                Assert.Equal(TimeSpan.FromHours(1), bodyObject.Duration);
                Assert.Equal(DisplayVisibility.Visible, bodyObject.CreationBehavior.DisplayExperience.Visibility);
                Assert.True(bodyObject.TriggeringBehavior.NotificationConfig.PlayAudible);
                Assert.IsType<NotifyOnlyOperation>(bodyObject.TriggeringBehavior.Operation);
            },new TimerResponse
            {
                Id = "ABC123",

            }));
            var notifyOnly = new CreateTimerRequest(TimeSpan.FromHours(1), new NotifyOnlyOperation(), DisplayVisibility.Visible, true);
            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            await client.Create(notifyOnly);
        }

        [Fact]
        public async Task GetCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/ABC123", req.RequestUri.ToString());
            }, new TimerResponse
            {
                Id = "ABC123",

            }));
            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            var response = await client.Get("ABC123");
            Assert.Equal("ABC123",response.Id);
        }

        [Fact]
        public async Task ListCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/", req.RequestUri.ToString());
            }, Utility.ExampleFileContent<ListTimerResponse>("ListTimer.json")));

            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            var response = await client.List();

            Assert.Equal(TimeSpan.FromMinutes(10),response.Timers.First().Duration);
            Assert.Equal(TimeSpan.FromMinutes(5), response.Timers.Skip(1).First().Duration);
            Assert.Equal(TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(3)), response.Timers.Skip(1).First().RemainingTimeWhenPaused);
            Assert.Equal(2,response.Timers.Length);
            Assert.Equal(2, response.TotalCount);
            Assert.Null(response.NextToken);
        }

        [Fact]
        public async Task DeleteCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Delete, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/", req.RequestUri.ToString());
            },HttpStatusCode.NoContent));

            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            await client.Delete();
        }

        [Fact]
        public async Task DeleteTimerCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Delete, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/ABC123", req.RequestUri.ToString());
            },HttpStatusCode.NoContent));

            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            await client.Delete("ABC123");
        }

        [Fact]
        public async Task PauseTimerCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/ABC123/pause", req.RequestUri.ToString());
            },HttpStatusCode.NoContent));

            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            await client.Pause("ABC123");
        }

        [Fact]
        public async Task ResumeTimerCall()
        {
            var http = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal("ABC123", req.Headers.Authorization.Parameter);
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal($"{TimersClient.EuropeEndpoint}/v1/alerts/timers/ABC123/resume", req.RequestUri.ToString());
            },HttpStatusCode.NoContent));

            var client = new TimersClient(TimersClient.EuropeEndpoint, "ABC123", http);
            await client.Resume("ABC123");
        }
    }
}
