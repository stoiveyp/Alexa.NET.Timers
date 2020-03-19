using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Timers.Creation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET
{
    public class TimersClient
    {
        public const string NorthAmericaEndpoint = "https://api.amazonalexa.com/v1/alerts/timers";
        public const string EuropeEndpoint = "https://api.eu.amazonalexa.com/v1/alerts/timers";
        public const string FarEastEndpoint = "https://api.fe.amazonalexa.com/v1/alerts/timers";

        private static readonly JsonSerializer Serializer;

        public HttpClient Client { get; set; }

        static TimersClient()
        {
            Serializer = JsonSerializer.CreateDefault();
        }

        public TimersClient(string endpointBase, string accessToken) : this(endpointBase, accessToken,
            new HttpClient())
        {

        }

        public TimersClient(string endpointBase, string accessToken, HttpClient client)
        {
            client = client ?? new HttpClient();
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(endpointBase, UriKind.Absolute);
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

        public Task<HttpResponseMessage> Create(CreateTimerRequest request)
        {
                var content = JObject.FromObject(request).ToString(Formatting.None);
               return Client.PostAsync(Client.BaseAddress,
                        new StringContent(content, Encoding.UTF8, "application/json"));
        }

        //public Task<HttpResponseMessage> Send<TAudienceType>(ProactiveEventRequest<TAudienceType> request) where TAudienceType : AudienceType
        //{
        //    if (string.IsNullOrWhiteSpace(request.ReferenceId))
        //    {
        //        throw new ArgumentNullException(nameof(request.ReferenceId));
        //    }

        //    var content = JObject.FromObject(request).ToString(Formatting.None);
        //    return Client.PostAsync(Client.BaseAddress,
        //            new StringContent(content, Encoding.UTF8, "application/json"));
        //}
    }
}
