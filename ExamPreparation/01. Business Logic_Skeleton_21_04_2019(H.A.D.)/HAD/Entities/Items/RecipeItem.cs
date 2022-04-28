using HAD.Contracts;
using System.Collections.Generic;


namespace HAD.Entities.Items
{
    public class RecipeItem : BaseItem, IRecipe
    {
     
        public RecipeItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitPointsBonus, long damageBonus, List<string> requiredItems) : base(name, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus, damageBonus)
        {
            RequiredItems = requiredItems;
        }

        public IReadOnlyList<string> RequiredItems { get; private set; }
    }
}
