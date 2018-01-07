using System;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Mappers.Auth;
using Newtonsoft.Json;

namespace CarTracker.Web.Util
{
    public class JsonUserConverter : JsonConverter<User>
    {
        public override void WriteJson(JsonWriter writer, User value, JsonSerializer serializer)
        {
            var vm = value.ToViewModel(); //Force this to serailize as the ViewModel
            serializer.Serialize(writer, vm);
        }

        public override User ReadJson(JsonReader reader, Type objectType, User existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return null; // Don't try to deserailize this
        }
    }
}
