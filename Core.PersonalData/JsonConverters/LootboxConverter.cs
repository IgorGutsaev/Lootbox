using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox.Core.JsonConverters
{
    public class ScLootboxConverter : JsonConverter<ScLootbox>
    {
        public override void WriteJson(JsonWriter writer, ScLootbox value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();

            Action<string, string> writeProperty = (name, val) => {
                writer.WritePropertyName(name);
                writer.WriteValue(val);
            };

            writeProperty("Version", value.Version);
            writeProperty("Created", value.Created.ToShortDateString());
            writeProperty("LastUpdate", value.LastUpdate.ToShortDateString());

            writer.WritePropertyName("Data");

            if (value.Fractions.Any())
            {
                var slotcnv = new ScSlotConverter();

                writer.WriteStartObject();
                foreach (var fr in value.Fractions)
                    new ScFractionConverter().WriteJson(writer, fr, serializer);
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        public override ScLootbox ReadJson(JsonReader reader, Type objectType, ScLootbox existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            ScLootbox result = new ScLootbox();
            result.Version = (string)obj["Version"];
            result.Created = Convert.ToDateTime(obj["Created"]);
            result.LastUpdate = Convert.ToDateTime(obj["LastUpdate"]);
            var fractions = DeserializeFractions(obj, serializer);

            foreach (var fr in fractions)
                result.Fractions.AppendFraction(fr);

            return result;
        }

        private IEnumerable<ScFraction> DeserializeFractions(JObject obj, JsonSerializer serializer)
        {
            JProperty prop = obj.Properties().Where(p => p.Name.StartsWith("Data")).First();
            JObject child = (JObject)prop.Value;

            foreach (JProperty jo in child.Children<JProperty>())
            {
                string identifier = jo.Name;
                ScFraction fr = new ScFraction() { Identifier = identifier };

                foreach (var t in jo)
                {
                    foreach (JProperty d in t)
                    {
                        ScSlot slot = d.ToObject<ScSlot>(serializer);
                        fr.AppendSlot(slot);
                    }
                }
                yield return fr;
            }
        }
    }
}