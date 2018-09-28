using Lootbox.Abstractions;

namespace Lootbox.Core
{
    public class ScSlot : Slot<ScSlot>
    {
        public string Identifier { get; set; }
        public object Value { get; set; }

        public static ScSlot Create(string identifier, object value)
        {
            return new ScSlot()
            {
                Identifier = identifier,
                Value = value
            };
        }
    }
}