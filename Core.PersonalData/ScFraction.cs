using Lootbox.Abstractions;
using System.Linq;

namespace Lootbox.Core
{
    public class ScFraction : Fraction<ScSlot>
    {
        public ScSlot this[string identifier]
        {
            get { return this.FirstOrDefault(s => string.Equals(s.Identifier, identifier, System.StringComparison.InvariantCultureIgnoreCase)); }
        }
    }
}