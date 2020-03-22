using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Timers.Creation
{
    public class OperationConverter : JsonConverter<Operation>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Operation value, JsonSerializer serializer)
        {

        }

        public override Operation ReadJson(JsonReader reader, Type objectType, Operation existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var commandType = jObject.Value<string>("type");
            var target = GetOperation(commandType);
            if (target != null)
            {
                serializer.Populate(jObject.CreateReader(), target);
                return target;
            }

            throw new ArgumentOutOfRangeException($"Operation type {commandType} not supported");
        }

        private Operation GetOperation(string commandType)
        {
            return commandType switch
            {
                "NOTIFY_ONLY" => new NotifyOnlyOperation(),
                "ANNOUNCE" => new AnnounceOperation(),
                "LAUNCH_TASK" => new LaunchTaskOperation(),
                _ => (Operation)null
            };
        }
    }
}
