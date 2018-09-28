using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lootbox.Abstractions
{
    public interface IJsonConvertersFabric
    {
        IEnumerable<JsonConverter> BuildConverters();
    }
}
