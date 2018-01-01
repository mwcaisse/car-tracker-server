using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarTracker.Web.Util
{
    public class JsonDateEpochConverter : JsonConverter<DateTime>
    {
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            long milliseconds = new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeMilliseconds();
            writer.WriteRawValue(milliseconds.ToString());
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            long milliseconds = Convert.ToInt64(reader.ReadAsString());
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        }
    }
    
}
