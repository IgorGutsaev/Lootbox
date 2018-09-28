using Newtonsoft.Json;

namespace Lootbox.Abstractions
{
    public interface ILootbox<Tf, T>
        where Tf: Fraction<T>
        where T : Slot<T>, new()
    {
        [JsonProperty(Order = 1)]
        string Version { get; }

        [JsonProperty("Fractions", Required = Required.Always, Order = 2)]
        FractionCollection<Tf, T> Fractions { get; }

        string ToJsonString();
    }
}
