using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Lootbox.Core.JsonConverters
{
    public class ScSlotConverter : JsonConverter<ScSlot>
    {
        public override void WriteJson(JsonWriter writer, ScSlot value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.Indented;
            writer.WritePropertyName(value.Identifier);
            writer.WriteValue(value.Value);
        }

        public override ScSlot ReadJson(JsonReader reader, Type objectType, ScSlot existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JProperty obj = JProperty.Load(reader);

            return ScSlot.Create(obj.Name, ((JValue)obj.Value).Value);
        }
    }
}