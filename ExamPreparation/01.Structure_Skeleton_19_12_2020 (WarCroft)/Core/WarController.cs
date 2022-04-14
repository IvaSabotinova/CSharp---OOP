using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private List<Character> characters;
        private List<Item> items;
        public WarController()
        {
            characters = new List<Character>();
            items = new List<Item>();
        }

        public IReadOnlyCollection<Character> Characters => characters;
        public IReadOnlyCollection<Item> Items => items;
        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];
            if (characterType != nameof(Priest) && characterType != nameof(Warrior))
            {
                throw new ArgumentException($"Invalid character type \"{characterType}\"!");
            }

            if (characterType == nameof(Priest))
            {
                characters.Add(new Priest(name));
            }
            if (characterType == nameof(Warrior))
            {
                characters.Add(new Warrior(name));
            }
            return $"{name} joined the party!";
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            if (itemName != nameof(HealthPotion) && itemName != nameof(FirePotion))
            {
                throw new ArgumentException($"Invalid item \"{itemName}\"!");
            }

            Item item = null;
            if (itemName == nameof(HealthPotion))
            {
                item = new HealthPotion();
            }
            if (itemName == nameof(FirePotion))
            {
                item = new FirePotion();
            }
            items.Add(item);
            return $"{itemName} added to pool.";
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            Character character = characters.FirstOrDefault(x => x.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }
            if (items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            Item itemToPick = items.LastOrDefault();
            character.Bag.AddItem(itemToPick);
            items.Remove(itemToPick);
            return $"{characterName} picked up {itemToPick.GetType().Name}!";
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];
            Character character = characters.FirstOrDefault(x => x.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }

            Item item = character.Bag.GetItem(itemName);
            character.UseItem(item);
            return $"{characterName} used {itemName}.";

        }

        public string GetStats()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Character character in characters.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
                string characterStatus = character.IsAlive ? "Alive" : "Dead";
                sb.AppendLine(
                    $"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {characterStatus}");
            }

            return sb.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];
            if (characters.All(x => x.Name != attackerName))
            {
                throw new ArgumentException($"Character {attackerName} not found!");
            }
            if (characters.All(x => x.Name != receiverName))
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }

            Character attacker = characters.Find(x => x.Name == attackerName);
            Character receiver = characters.Find(x => x.Name == receiverName);
            if (attacker.GetType().Name == nameof(Priest))
            {
                throw new ArgumentException($"{attacker.Name} cannot attack!");
            }
            IAttacker attacker1 = attacker as IAttacker;
            attacker1.Attack(receiver);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{attacker.Name} attacks {receiver.Name} for {attacker.AbilityPoints} hit points! {receiver.Name} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");

            if (!receiver.IsAlive)
            {
                sb.AppendLine($"{receiver.Name} is dead!");
            }

            return sb.ToString().TrimEnd();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];
            if (characters.All(x => x.Name != healerName))
            {
                throw new ArgumentException($"Character {healerName} not found!");
            }
            if (characters.All(x => x.Name != healingReceiverName))
            {
                throw new ArgumentException($"Character {healingReceiverName} not found!");
            }

            Character healer = characters.Find(x => x.Name == healerName);
            Character healingReceiver = characters.Find(x => x.Name == healingReceiverName);
            if (!healer.IsAlive || !healingReceiver.IsAlive)
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
            if (healer.GetType().Name != nameof(Priest))
            {
                throw new ArgumentException($"{healerName} cannot heal!");
            }
            IHealer healer1 = healer as IHealer;
            healer1.Heal(healingReceiver);
            return
                $"{healer.Name} heals {healingReceiver.Name} for {healer.AbilityPoints}! {healingReceiver.Name} has {healingReceiver.Health} health now!";
        }
    }
}
