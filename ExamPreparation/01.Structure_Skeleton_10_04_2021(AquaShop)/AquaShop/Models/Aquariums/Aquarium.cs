using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private readonly List<IDecoration> decorations;
        private readonly List<IFish> fishes;

        protected Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            decorations = new List<IDecoration>();
            fishes = new List<IFish>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                name = value;
            }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                capacity = value;
            }
        }

        public int Comfort => decorations.Sum(x => x.Comfort);
        public ICollection<IDecoration> Decorations => decorations;
        public ICollection<IFish> Fish => fishes;
        public void AddFish(IFish fish)
        {
            if (fishes.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            fishes.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            return fishes.Remove(fish);
        }

        public void AddDecoration(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public void Feed()
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                fishes[i].Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            string result = fishes.Count > 0 ? string.Join(", ", fishes.Select(x => x.Name)) : "none";
            sb.AppendLine($"{Name} ({GetType().Name}):");
            sb.AppendLine($"Fish: {result}");
            sb.AppendLine($"Decorations: {decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");
            return sb.ToString().TrimEnd();

        }
    }
}
