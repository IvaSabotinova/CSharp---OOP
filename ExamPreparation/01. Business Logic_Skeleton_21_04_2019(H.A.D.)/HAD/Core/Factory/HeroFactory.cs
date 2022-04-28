using HAD.Contracts;
using HAD.Core.Factory.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace HAD.Core.Factory
{
    public class HeroFactory : IHeroFactory
    {

        public IHero CreateHero(string heroType, string name)
        {
            Type type = Assembly.GetCallingAssembly().GetTypes().First(x => x.Name == heroType);
            IHero hero = Activator.CreateInstance(type, name) as IHero;
            return hero;
            //IHero hero = null;

            //switch (heroType)
            //{
            //    case "Assassin":
            //        hero = new Assassin(name);
            //        break;
            //    case "Barbarian":
            //        hero = new Barbarian(name);
            //        break;
            //    case "Wizard":
            //        hero = new Wizard(name);
            //        break;
            //}

            //return hero;
        }
    }
}
