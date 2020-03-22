using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Timers;
using Alexa.NET.Timers.Creation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET
{
    public class TimersClient
    {
        public const string NorthAmericaEndpoint = "https://api.amazonalexa.com";
        public const string EuropeEndpoint = "https://api.eu.amazonalexa.com";
        public const string FarEastEndpoint = "https://api.fe.amazonalexa.com";

        private static readonly JsonSerializer Serializer;

        public HttpClient Client { get; set; }

        static TimersClient()
        {
            Serializer = JsonSerializer.CreateDefault();
        }

        public TimersClient(SkillRequest request) : this(request,null)
        {

        }

        public TimersClient(SkillRequest request, HttpClient client):this(request.Context.System.ApiEndpoint,request.Context.System.ApiAccessToken,client)
        {

        }

        public TimersClient(string endpointBase, string accessToken) : this(endpointBase, accessToken,
            new HttpClient())
        {

        }

        public TimersClient(string endpointBase, string accessToken, HttpClient client)
        {
            client ??= new HttpClient();
            if (client.BaseAddress == null)
            {
                var baseUri = new Uri(endpointBase, UriKind.Absolute);
                var builder = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "v1/alerts/timers/");
                client.BaseAddress = builder.Uri;
            }

            if (client.DefaultRequestHeaders.Authorization == null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            Client = client;
        }

        public TimersClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<TimerResponse> Create(CreateTimerRequest request)
        {
            var content = JObject.FromObject(request).ToString(Formatting.None);
            var response = await Client.PostAsync(string.Empty,
                     new StringContent(content, Encoding.UTF8, "application/json"));
            return await response.BodyOrError(JsonConvert.DeserializeObject<TimerResponse>, HttpStatusCode.OK);
        }

        public async Task<TimerResponse> Get(string id)
        {
            var response = await Client.GetAsync(new Uri($"{id}", UriKind.Relative), HttpCompletionOption.ResponseContentRead);
            return await response.BodyOrError(JsonConvert.DeserializeObject<TimerResponse>, HttpStatusCode.OK);
        }

        public async Task<ListTimerResponse> List()
        {
            var response = await Client.GetAsync(string.Empty, HttpCompletionOption.ResponseContentRead);
            var stream = await response.Content.ReadAsStreamAsync();
            return await response.BodyOrError(JsonConvert.DeserializeObject<ListTimerResponse>, HttpStatusCode.OK);
        }

        public async Task Delete()
        {
            var response = await Client.DeleteAsync(string.Empty);
            await response.CodeOrError(HttpStatusCode.NoContent);
        }

        public async Task Delete(string id)
        {
            var response = await Client.DeleteAsync(new Uri($"{id}", UriKind.Relative));
            await response.CodeOrError(HttpStatusCode.NoContent);
        }

        public async Task Pause(string id)
        {
            var response = await Client.PostAsync(new Uri($"{id}/pause", UriKind.Relative), null);
            await response.CodeOrError(HttpStatusCode.NoContent);
        }

        public async Task Resume(string id)
        {
            var response = await Client.PostAsync(new Uri($"{id}/resume",UriKind.Relative), null);
            await response.CodeOrError(HttpStatusCode.NoContent);
        }
    }
}
