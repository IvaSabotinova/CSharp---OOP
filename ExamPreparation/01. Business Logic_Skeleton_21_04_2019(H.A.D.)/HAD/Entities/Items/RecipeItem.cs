using HAD.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace HAD.Entities.Items
{
    public class RecipeItem : BaseItem, IRecipe
    {
        private List<string> requiredItems;
        public RecipeItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitPointsBonus, long damageBonus, params string[] requireditems) : base(name, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus, damageBonus)
        {
            this.requiredItems = requireditems.ToList();
        }

        public IReadOnlyList<string> RequiredItems => requiredItems;
    }
}
