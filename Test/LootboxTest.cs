using Lootbox.Abstractions;
using Lootbox.Core;
using Lootbox.Core.JsonConverters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Lootbox.Test
{
    public class LootboxTest
    {
        private readonly ITestOutputHelper output;
        private readonly ILootboxSerializer<ILootbox<ScFraction, ScSlot>> serializer = null;

        public LootboxTest(ITestOutputHelper output)
        {
            serializer = new LootboxDefaultSerializer<ScFraction, ScSlot>(
               new JsonLootboxSerializeStrategy<ScFraction, ScSlot>(new ScSlotConverter()
               , new ScFractionConverter()
               , new ScLootboxConverter()));

            this.output = output;
        }

        [Theory]
        [MemberData(nameof(ScLootboxTest.ScLootboxSet), MemberType = typeof(ScLootboxTest))]
        public void Test_Serialization_Deserialization(ScLootbox lootbox)
        {
            // Pre-validate
            Assert.NotNull(lootbox);
            Assert.NotNull(serializer);

            // Perform
            string serialized = serializer.Serialize(lootbox);
            var deserialized = serializer.Deserialize<ScLootbox>(serialized);

            // Post-validate
            Assert.NotNull(deserialized);
            Assert.NotNull(lootbox.Fractions);
            Assert.Equal(lootbox.Fractions.Count, deserialized.Fractions.Count);
            foreach (var fraction in lootbox.Fractions)
            {
                ScFraction originalFraction = lootbox.Fractions[fraction.Identifier] as ScFraction;
                Assert.NotNull(originalFraction);
                Assert.Equal(originalFraction.Count, fraction.Count); // Slots count

                foreach (var slot in fraction)
                {
                    ScSlot originalSlot = originalFraction[slot.Identifier];
                    Assert.NotNull(originalSlot);
                    Assert.Equal(originalSlot.Value, slot.Value);
                }
            }
        }

        [Theory]
        [InlineData("1990/01/01", "2001/01/01")]
        [InlineData("", "1900/01/01")]
        [InlineData("2009/01/01", "")]
        public void Test_Goal_Group_By_Birthday(string fromDate, string toDate)
        {
            // Prepare
            DateTime from = string.IsNullOrWhiteSpace(fromDate) ? DateTime.MinValue : DateTime.Parse(fromDate);
            DateTime to = string.IsNullOrWhiteSpace(toDate) ? DateTime.MaxValue : DateTime.Parse(toDate);
            long fromInTicks = from.Ticks;
            long endInTicks = to.Ticks;
            JSchema schema = ScLootboxTest.BasicSchema();
            JSchema birthdaySchema = schema.Properties["Data"].Properties["SocDemog"].Properties["Birthday"];
            IEnumerable<string> personalDatas = ScLootboxTest.ScLootboxJsonSet().Select(d => d.First().ToString());
            IList<string> matchingDatas = new List<string>();

            // Pre-validate
            Assert.True(to >= from);
            Assert.NotNull(schema);
            Assert.NotNull(birthdaySchema);
            Assert.NotEmpty(personalDatas);

            // Perform
            birthdaySchema.Minimum = from.Ticks;
            birthdaySchema.Maximum = to.Ticks;

            foreach (var data in personalDatas)
            {
                JObject jobj = JObject.Parse(data);

                IList<string> jsonSchemaMessages = new List<string>();
                bool jsonSchemaIsValid = jobj.IsValid(schema, out jsonSchemaMessages);
                if (jsonSchemaIsValid)
                    matchingDatas.Add(data);
            }

            // Post-validate
            IEnumerable<ScLootbox> result = matchingDatas.Select(m => serializer.Deserialize<ScLootbox>(m))
                .Where(x => x.Fractions["SocDemog"]
                    .Where(s => s.Identifier == "Birthday"
                        && ((long)s.Value < fromInTicks || (long)s.Value > endInTicks)).Any());

            Assert.Empty(result);
            output.WriteLine($"Customer count: {matchingDatas.Count}.");
            int count = 1;
            foreach (var data in matchingDatas)
            {
                output.WriteLine(String.Empty);
                output.WriteLine($"Customer № {count.ToString()}:");
                output.WriteLine(data);
            }
        }

        [Theory]
        [InlineData("Иван")]
        [InlineData("Борис")]
        [InlineData("Ирина")]
        public void Test_Goal_Group_By_Name(string name)
        {
            // Prepare
            JSchema schema = ScLootboxTest.BasicSchema();
            JSchema nameSchema = schema.Properties["Data"].Properties["SocDemog"].Properties["Name"];
            IEnumerable<string> personalDatas = ScLootboxTest.ScLootboxJsonSet().Select(d => d.First().ToString());
            IList<string> matchingDatas = new List<string>();
            IEnumerable<string> nameInvariants = new string[] { name, name.ToLower(), name.ToUpper() };

            // Pre-validate
            Assert.NotEmpty(name);
            Assert.NotNull(schema);
            Assert.NotNull(nameSchema);
            Assert.NotEmpty(personalDatas);

            // Perform
            foreach (var n in nameInvariants)
                nameSchema.Enum.Add(n);

            foreach (var data in personalDatas)
            {
                JObject jobj = JObject.Parse(data);

                IList<string> jsonSchemaMessages = new List<string>();
                bool jsonSchemaIsValid = jobj.IsValid(schema, out jsonSchemaMessages);
                if (jsonSchemaIsValid)
                    matchingDatas.Add(data);
            }

            // Post-validate
            IEnumerable<ScLootbox> result = matchingDatas.Select(m => serializer.Deserialize<ScLootbox>(m))
                .Where(x => x.Fractions["SocDemog"]
                    .Where(s => s.Identifier == "Name"
                        && !nameInvariants.Contains(s.Value)).Any());

            Assert.Empty(result);
            output.WriteLine($"Customer count: {matchingDatas.Count}.");
            int count = 1;
            foreach (var data in matchingDatas)
            {
                output.WriteLine(String.Empty);
                output.WriteLine($"Customer № {count.ToString()}:");
                output.WriteLine(data);
            }
        }

        [Theory]
        [MemberData(nameof(ScLootboxTest.ScLootboxJsonSet), MemberType = typeof(ScLootboxTest))]
        public void Test_GenerateSchema(string json)
        {
            JSchema schema = ScLootboxTest.BasicSchema();

            schema.Properties["Data"].Properties["SocDemog"].Properties["Birthday"]
                .Minimum = new DateTime(2000, 1, 1).Ticks;

            schema.Properties["Data"].Properties["SocDemog"].Properties["Birthday"]
                .Maximum = new DateTime(2001, 1, 1).Ticks;

            var nameProperty = schema.Properties["Data"].Properties["SocDemog"].Properties["Name"];
            nameProperty.Enum.Add("Иван");
            nameProperty.Enum.Add("иван");

            Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(json);

            IList<string> jsonSchemaMessages = new List<string>();
            bool jsonSchemaIsValid = jobj.IsValid(schema, out jsonSchemaMessages);
        }

    }
}
