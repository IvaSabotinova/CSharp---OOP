using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easter.Core.Contracts;
using Easter.Models;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;
        private int countOfColoredEggs;


        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
            countOfColoredEggs = 0;
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType == "HappyBunny")
            {
                bunnies.Add(new HappyBunny(bunnyName));
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunnies.Add(new SleepyBunny(bunnyName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            return String.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);

        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            bunny.Dyes.Add(new Dye(power));
            return String.Format(OutputMessages.DyeAdded, power, bunny.Name);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return String.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            List<IBunny> suitableBunnies =
                bunnies.Models.OrderByDescending(x => x.Energy).Where(x => x.Energy >= 50).ToList();
            if (suitableBunnies.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            
            while (suitableBunnies.Count > 0)
            {
                IWorkshop workshop = new Workshop();
                IBunny bunny = suitableBunnies.FirstOrDefault(x=>x.Dyes.Count > 0);
                if (bunny == null)
                {
                    break;
                }
                workshop.Color(eggs.FindByName(eggName), bunny);
                if (bunny.Energy <= 0)
                {
                    suitableBunnies.Remove(bunny);
                }
                
                if (eggs.FindByName(eggName).IsDone())
                {
                    break;
                }

            }

            string result = eggs.FindByName(eggName).IsDone() ? "done" : "not done";
            countOfColoredEggs = eggs.FindByName(eggName).IsDone() ? countOfColoredEggs + 1 : countOfColoredEggs + 0;
            return $"Egg {eggName} is {result}.";

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{countOfColoredEggs} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (IBunny bunny in bunnies.Models.Where(x=>x.Energy > 0))
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
