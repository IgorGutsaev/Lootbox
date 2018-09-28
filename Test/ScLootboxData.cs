using Lootbox.Abstractions;
using Lootbox.Core;
using Lootbox.Core.JsonConverters;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lootbox.Test
{
    public class ScLootboxData
    {
        private static IEnumerable<ScLootbox> Lootboxes()
        {
            yield return new ScLootboxBuilder()
                    .AddFractions((l) =>
                        l.AppendFraction(new ScFraction { Identifier = "SocDemog" }
                            .AppendSlot(ScSlot.Create("Name", "Иван"))
                            .AppendSlot(ScSlot.Create("Birthday", new DateTime(2000, 9, 26).Ticks)))
                        .AppendFraction(new ScFraction { Identifier = "Consumer" })
                    )
                    .SetVersion("1.0.0")
                    .SetStartDate(DateTime.Now)
                    .Build() as ScLootbox;

            yield return new ScLootboxBuilder()
                    .AddFractions((l) =>
                        l.AppendFraction(new ScFraction { Identifier = "SocDemog" }
                            .AppendSlot(ScSlot.Create("Name", "Толик"))
                            .AppendSlot(ScSlot.Create("Birthday", new DateTime(2005, 7, 31).Ticks)))
                        .AppendFraction(new ScFraction { Identifier = "Consumer" })
                    )
                    .SetVersion("1.0.0")
                    .SetStartDate(DateTime.Now)
                    .Build() as ScLootbox;

            yield return new ScLootboxBuilder()
                    .AddFractions((l) =>
                        l.AppendFraction(new ScFraction { Identifier = "SocDemog" }
                            .AppendSlot(ScSlot.Create("Name", "Василий"))
                            .AppendSlot(ScSlot.Create("Birthday", new DateTime(1988, 2, 14).Ticks)))
                        .AppendFraction(new ScFraction { Identifier = "Consumer" })
                    )
                    .SetVersion("1.0.0")
                    .SetStartDate(DateTime.Now)
                    .Build() as ScLootbox;

            yield return new ScLootboxBuilder()
                    .AddFractions((l) =>
                        l.AppendFraction(new ScFraction { Identifier = "SocDemog" }
                            .AppendSlot(ScSlot.Create("Name", "Ирина"))
                            .AppendSlot(ScSlot.Create("Birthday", new DateTime(2009, 11, 1).Ticks)))
                        .AppendFraction(new ScFraction { Identifier = "Consumer" })
                    )
                    .SetVersion("1.0.0")
                    .SetStartDate(DateTime.Now)
                    .Build() as ScLootbox;
        }

        public static IEnumerable<object[]> ScLootboxSet()
        {
            foreach (var l in Lootboxes())
                yield return new object[] { l };
        }

        public static IEnumerable<object[]> ScLootboxJsonSet(ILootboxSerializer serializer)
        {
            foreach (var l in Lootboxes())
                yield return 
                    new object[] { serializer.Serialize(l) };
        }

        public static JSchema BasicSchema()
        {
            //JSchema schema = null;
            //using (StreamReader sch = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(basicSchema))))
            //using (JsonTextReader reader = new JsonTextReader(sch))
            //{
            //    schema = JSchema.Load(reader);
            //}

            return new JSchema()
            {
                Type = JSchemaType.Object,
                Properties =
                {
                    { "Created", new JSchema { Type = JSchemaType.String } },
                    {
                        "Data", new JSchema
                        {
                            Type = JSchemaType.Object,
                            Properties =
                            {
                                { "SocDemog", new JSchema {
                                        Type = JSchemaType.Object,
                                        Properties =
                                        {
                                            { "Name", new JSchema { Type = JSchemaType.String } },
                                            { "Birthday", new JSchema { Type = JSchemaType.Number } }
                                        },
                                        Required = { "Birthday" },
                                        AllowAdditionalProperties = false
                                    }
                                },
                                { "Consumer",
                                    new JSchema {
                                        Type = JSchemaType.Object,
                                        Properties = { }
                                    }
                                }
                            }
                            , Required = { "SocDemog", "Consumer" },
                            AllowAdditionalProperties = false
                        }
                    }
                }
            };
        }
    }
}
