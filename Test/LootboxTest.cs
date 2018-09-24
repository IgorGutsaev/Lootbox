using Lootbox.Abstractions;
using Lootbox.Core;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lootbox.Test
{
    public class LootboxTest
    {
        [Theory]
        [MemberData(nameof(ScLootboxToolkit.ScLootboxSet), MemberType = typeof(ScLootboxToolkit))]
        public void Test_GenerateSet(ScLootbox lootbox)
        {
            var ser = new LootboxSerializer<ScLootbox, ScFraction, ScSlot>(
                new JsonLootboxSerializeStrategy<ScFraction, ScSlot>());

            string serialized = ser.Serialize(lootbox);
            var des = ser.Deserialize(serialized);
        }

        [Theory]
        [MemberData(nameof(ScLootboxToolkit.ScLootboxJsonSet), MemberType = typeof(ScLootboxToolkit))]
        public void Test_GenerateSchema(string json)
        {
            JSchemaGenerator generator = new JSchemaGenerator();

            // types with no defined ID have their type name as the ID
            generator.SchemaIdGenerationHandling = SchemaIdGenerationHandling.TypeName;

            JSchema schema = generator.Generate(typeof(ScLootbox));
            schema.Properties["Fractions"]
                .Items
                .First()
                .Properties["Identifier"]
                .AllOf
                .Add(new JSchema { Type = JSchemaType.String, Enum = { "SocDemog", "Consumer" } });

            //schema.Properties["Slots"]
            //    .Items
            //    .First()
            //    .Properties["Identifier"]
            //    .OneOf
            //    .Add(new JSchema { Type = JSchemaType.String, Enum = { "Age" }  });


            Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(json);

            IList<string> jsonSchemaMessages = new List<string>();
            bool jsonSchemaIsValid = jobj.IsValid(schema, out jsonSchemaMessages);
        }
    }
}
