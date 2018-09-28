using Newtonsoft.Json;

namespace Lootbox.Abstractions
{
    public interface ILootbox<T>
        where T : Slot<T>, new()
    {
        [JsonProperty(Order = 1)]
        string Version { get; }

        [JsonProperty("Fractions", Required = Required.Always, Order = 2)]
        FractionCollection<T> Fractions { get; }

        string ToJsonString();
    }
}
