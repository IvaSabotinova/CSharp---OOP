using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorations;
        private readonly List<IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquariums.Add(new FreshwaterAquarium(aquariumName));
            }
            else if (aquariumType == nameof(SaltwaterAquarium))
            {
                aquariums.Add(new SaltwaterAquarium(aquariumName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            return String.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType == nameof(Ornament))
            {
                decorations.Add(new Ornament());
            }
            else if (decorationType == nameof(Plant))
            {
                decorations.Add(new Plant());
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            return String.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            IDecoration decoration = decorations.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentDecoration,
                    decorationType));
            }

            decorations.Remove(decoration);
            aquarium.AddDecoration(decoration);
            return String.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);

        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            IFish fish = null;
            if (fishType != nameof(FreshwaterFish) && fishType != nameof(SaltwaterFish))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            if ((fishType == nameof(FreshwaterFish) && aquarium.GetType().Name != nameof(FreshwaterAquarium)) || (fishType == nameof(SaltwaterFish) && aquarium.GetType().Name != nameof(SaltwaterAquarium)))
            {
                return String.Format(OutputMessages.UnsuitableWater);
            }

            if (fishType == nameof(FreshwaterFish) && aquarium.GetType().Name == nameof(FreshwaterAquarium))
            {
                aquarium.AddFish(new FreshwaterFish(fishName, fishSpecies, price));
            }
            else if (fishType == nameof(SaltwaterFish) && aquarium.GetType().Name == nameof(SaltwaterAquarium))
            {
                aquarium.AddFish(new SaltwaterFish(fishName, fishSpecies, price));
            }
            return String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            aquarium.Feed();
            return String.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            decimal totalPriceDecorations = aquarium.Decorations.Sum(x => x.Price);
            decimal totalPriceFishes = aquarium.Fish.Sum(x => x.Price);
            return String.Format(OutputMessages.AquariumValue, aquariumName, $"{(totalPriceDecorations + totalPriceFishes):f2}");

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IAquarium aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
