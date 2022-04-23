using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;
        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            if (weapons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            if (type != nameof(Barbarian) && type != nameof(Knight))
            {
                throw new InvalidOperationException("Invalid hero type.");
            }
            IHero hero = null;
            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return $"Successfully added Barbarian {name} to the collection.";
            }
            hero = new Knight(name, health, armour);
            heroes.Add(hero);
            return $"Successfully added Sir {name} to the collection.";

        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }
            if (type != nameof(Claymore) && type != nameof(Mace))
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }
            IWeapon weapon = null;
            if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);

            }
            else if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            weapons.Add(weapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IHero hero in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                string weaponYesOrNo = hero.Weapon != null ? $"{hero.Weapon.Name}" : "Unarmed";
                sb.AppendLine($"--Weapon: {weaponYesOrNo}");
            }
            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            IMap map = new Map();
            IList<IHero> players = new List<IHero>(heroes.Models.Where(x => x.Weapon != null));
            return map.Fight(players);
        }
    }
}
