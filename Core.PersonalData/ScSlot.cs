using Lootbox.Abstractions;

namespace Lootbox.Core
{
    public class ScSlot : Slot<ScSlot>
    {
        public string Identifier { get; set; }
        public string Value { get; set; }

        public static ScSlot Create(string identifier, string value)
        {
            return new ScSlot()
            {
                Identifier = identifier,
                Value = value
            };
        }
    }
}
