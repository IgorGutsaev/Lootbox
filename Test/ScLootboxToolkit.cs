using Lootbox.Core;
using System.Collections.Generic;

namespace Lootbox.Test
{
    public class ScLootboxToolkit
    {
        public static IEnumerable<object[]> ScLootboxSet()
        {
            yield return
                new object[] { new ScLootboxBuilder()
                    .AddFractions((l) =>
                
                        l.AppendFraction(new ScFraction { Identifier = "SocDemog" }
                            .AppendSlot(ScSlot.Create("Age", "18"))
                            .AppendSlot(null))                            
                        .AppendFraction(new ScFraction { Identifier = "Consumer" })
                    )
                    .SetVersion("1.0.0")
                    .Build() };
        }
    }
}
