using Newtonsoft.Json;
using System;

namespace Lootbox.Core.JsonConverters
{
    public class ScFractionConverter : JsonConverter<ScFraction>
    {
        public override void WriteJson(JsonWriter writer, ScFraction value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.Indented;

            writer.WritePropertyName(value.Identifier);
            writer.WriteStartObject();
            var enumerator = value.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                var slotcnv = new ScSlotConverter();

                while (enumerator.Current != null)
                {
                    slotcnv.WriteJson(writer, enumerator.Current, serializer);
                    enumerator.MoveNext();
                }
            }
            writer.WriteEndObject();
        }

        public override ScFraction ReadJson(JsonReader reader, Type objectType, ScFraction existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}