using Lootbox.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lootbox.Core.JsonConverters
{
    public class ScJsonConvertersFabric : IJsonConvertersFabric
    {
        public IEnumerable<JsonConverter> BuildConverters()
        {
            yield return new ScSlotConverter();
            yield return new ScFractionConverter();
            yield return new ScLootboxConverter();
        }
    }
}
