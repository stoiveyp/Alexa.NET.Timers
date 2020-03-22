﻿using System;
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
            client ??= new HttpClient();
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

        public async Task<TimerResponse> Create(CreateTimerRequest request)
        {
            var content = JObject.FromObject(request).ToString(Formatting.None);
            var response = await Client.PostAsync(Client.BaseAddress,
                     new StringContent(content, Encoding.UTF8, "application/json"));
            var stream = await response.Content.ReadAsStreamAsync();
            return Serializer.Deserialize<TimerResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task<TimerResponse> Get(string id)
        {
            var response = await Client.GetAsync(new Uri(Client.BaseAddress, new Uri($"/{id}",UriKind.Relative)), HttpCompletionOption.ResponseContentRead);
            var stream = await response.Content.ReadAsStreamAsync();
            return Serializer.Deserialize<TimerResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task<ListTimerResponse> List()
        {
            var response = await Client.GetAsync(Client.BaseAddress, HttpCompletionOption.ResponseContentRead);
            var stream = await response.Content.ReadAsStreamAsync();
            return Serializer.Deserialize<ListTimerResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task Delete()
        {
            var response = await Client.DeleteAsync(Client.BaseAddress);
            await response.CodeOrError(HttpStatusCode.OK);
        }

        public async Task Delete(string id)
        {
            var response = await Client.DeleteAsync(new Uri(Client.BaseAddress, new Uri($"/{id}", UriKind.Relative)));
            await response.CodeOrError(HttpStatusCode.OK);
        }

        public async Task Pause(string id)
        {
            var response = await Client.PostAsync(new Uri(Client.BaseAddress, new Uri($"/{id}/pause", UriKind.Relative)),null);
            await response.CodeOrError(HttpStatusCode.OK);
        }

        public async Task Resume(string id)
        {
            var response = await Client.PostAsync(new Uri(Client.BaseAddress, new Uri($"/{id}/resume", UriKind.Relative)),null);
            await response.CodeOrError(HttpStatusCode.OK);
        }
    }
}
