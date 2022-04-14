using System;
using System.Collections.Generic;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private ICollection<IEquipment> equipments;
        private ICollection<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipments = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }

                name = value;
            }
        }
        public int Capacity
        {
            get { return capacity; }
            private set { capacity = value; }
        }

        public double EquipmentWeight
        {
            get
            {
                double gymEquipmentWeight = 0;
                foreach (IEquipment equip in equipments)
                {
                    gymEquipmentWeight += equip.Weight;
                }
                return gymEquipmentWeight;
            }

        }
        public ICollection<IEquipment> Equipment => equipments;
        public ICollection<IAthlete> Athletes => athletes;
        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return athletes.Remove(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.equipments.Add(equipment);
        }

        public void Exercise()
        {
            foreach (IAthlete athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {

            List<string> athletesNames = new List<string>();

            foreach (IAthlete athlete in athletes)
            {
                athletesNames.Add(athlete.FullName);

            }

            string result = this.athletes.Count > 0 ? string.Join(", ", athletesNames) : "No athletes";
            return $"{Name} is a {this.GetType().Name}:" + Environment.NewLine +
                   $"Athletes: {result}" + Environment.NewLine +
                   $"Equipment total count: {equipments.Count}" + Environment.NewLine +
                   $"Equipment total weight: {EquipmentWeight:f2} grams";

        }
    }
}
