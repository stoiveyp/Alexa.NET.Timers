﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alexa.NET.Timers
{
    internal static class StatusResponse
    {
        public static async Task CodeOrError(this HttpResponseMessage response, HttpStatusCode expectedCode)
        {
            if (response.StatusCode == expectedCode)
            {
                return;
            }

            var body = string.Empty;
            if (response.Content != null)
            {
                body = await response.Content.ReadAsStringAsync();
            }

            throw new InvalidOperationException(
                $"Expected Status Code {(int)expectedCode}. Received {(int)response.StatusCode}. Response Body: {body}");

        }

        public static async Task<T> BodyOrError<T>(this HttpResponseMessage response, Func<string, T> converter,
            HttpStatusCode expectedCode)
        {
            var body = string.Empty;
            if (response.Content != null)
            {
                body = await response.Content.ReadAsStringAsync();
            }

            if (response.StatusCode != expectedCode)
            {
                throw new InvalidOperationException(
                    $"Expected Status Code {(int)expectedCode}. Received {(int)response.StatusCode}. Response Body: {body}");
            }

            return converter(body);
        }
    }
}
