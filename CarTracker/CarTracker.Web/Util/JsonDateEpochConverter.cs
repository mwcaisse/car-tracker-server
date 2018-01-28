using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarTracker.Web.Util
{
    public class JsonDateEpochConverter : JsonConverter
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            if (date.HasValue)
            {
                long milliseconds = new DateTimeOffset(date.Value.ToUniversalTime()).ToUnixTimeMilliseconds();
                writer.WriteRawValue(milliseconds.ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var stringVal = reader.Value.ToString();
            if (string.IsNullOrWhiteSpace(stringVal))
            {
                return null;
            }
            long milliseconds = Convert.ToInt64(stringVal);
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime.ToLocalTime();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}
