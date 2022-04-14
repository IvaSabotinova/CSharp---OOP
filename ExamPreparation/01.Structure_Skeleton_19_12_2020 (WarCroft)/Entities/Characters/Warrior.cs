using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {

        public Warrior(string name) : base(name, 100, 50, 40, new Satchel())
        {
        }
        public void Attack(Character character)
        {
            if (!character.IsAlive || !this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }

            if (this == character)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }
            character.TakeDamage(this.AbilityPoints);
           

        }

    }
}
