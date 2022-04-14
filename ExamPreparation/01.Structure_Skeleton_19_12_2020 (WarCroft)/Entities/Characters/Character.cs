using System;

using WarCroft.Constants;
using WarCroft.Entities.Items;


namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;
       private bool isAlive;
        private Bag bag;

        protected Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            Health = health;
            Armor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;
            IsAlive = true;
            BaseArmor = armor;
            BaseHealth = health;
        }
        public double BaseHealth
        {
            //the starting and also the maximum health a character can have
            get { return baseHealth; } private set { baseHealth = value; }
        }


        public double Health
        {
            get { return health; }
            internal set
            {
                
                if (value < 0)
                {
                    health = 0;
                }

                if (value > BaseHealth)
                {
                    health = baseHealth;
                }
                health = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }

                name = value;
            }
        }

        public double BaseArmor
        {
            //the starting armor
            get { return baseArmor; }
            private set { baseArmor = value; }
        }

        public double Armor
        {
           
            get { return armor; }
            private set
            {
               
                if (value < 0)
                {
                    armor = 0;
                }

                armor = value;
            }
        }

        public double AbilityPoints
        {
            get { return abilityPoints; }
            set { abilityPoints = value; }
        }
        public Bag Bag
        {
            get { return bag; }
            set { bag = value; }
        }
        public bool IsAlive
        {
            get { return isAlive;}
            set { isAlive = value; }
        }

        public void TakeDamage(double hitPoints)
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
            
            if (Armor - hitPoints < 0)
            {
                double armorLeft = hitPoints - Armor;
                Health -= armorLeft;
                Armor = 0;
                if (Health < 0)
                {
                    Health = 0;
                    IsAlive = false;
                }
            }
            else
            {
                Armor -= hitPoints;
            }
        }
        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }

        }

        public void UseItem(Item item)
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
            item.AffectCharacter(this);
        }
       
    }
}