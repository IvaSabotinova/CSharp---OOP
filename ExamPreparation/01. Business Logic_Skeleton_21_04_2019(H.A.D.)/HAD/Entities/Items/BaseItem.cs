using System;
using System.Text;
using HAD.Contracts;

namespace HAD.Entities.Items
{
    public abstract class BaseItem : IItem
    {
        private string name;

        private long strengthBonus;

        private long agilityBonus;

        private long intelligenceBonus;

        private long hitPointsBonus;

        private long damageBonus;
        protected BaseItem(string name, long strengthBonus, long agilityBonus, long intelligenceBonus, long hitPointsBonus, long damageBonus)
        {
            this.Name = name;
            this.StrengthBonus = strengthBonus;
            this.AgilityBonus = agilityBonus;
            this.IntelligenceBonus = intelligenceBonus;
            this.HitPointsBonus = hitPointsBonus;
            this.DamageBonus = damageBonus;
        }

        public string Name
        {
            get => name;
            private set
            { name = value; }
        }

        public long StrengthBonus
        {
            get { return strengthBonus; }
            private set { strengthBonus = value; }
        }

        public long AgilityBonus
        {
            get { return agilityBonus; }
            private set { agilityBonus = value; }
        }

        public long IntelligenceBonus
        {
            get { return intelligenceBonus; }
            private set { intelligenceBonus = value; }
        }

        public long HitPointsBonus
        {
            get { return hitPointsBonus; }
            private set { hitPointsBonus = value; }
        }

        public long DamageBonus
        {
            get { return damageBonus; }
            private set { damageBonus = value; }
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"###+{this.StrengthBonus} Strength");
            result.AppendLine($"###+{this.AgilityBonus} Agility");
            result.AppendLine($"###+{this.IntelligenceBonus} Intelligence");
            result.AppendLine($"###+{this.HitPointsBonus} HitPoints");
            result.Append($"###+{this.DamageBonus} Damage");

            return result.ToString();
        }
    }
}