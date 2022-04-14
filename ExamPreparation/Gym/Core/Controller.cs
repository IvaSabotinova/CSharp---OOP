using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;

namespace Gym.Core
{

    public class Controller : IController
    {

        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        //public IReadOnlyCollection<IGym> Gyms => this.gyms;

        //public IReadOnlyCollection<EquipmentRepository> Equipment =>
        //    (IReadOnlyCollection<EquipmentRepository>)equipment;
        public string AddGym(string gymType, string gymName)
        {
            if (gymType != "BoxingGym" && gymType != "WeightliftingGym")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            if (gymType == "BoxingGym")
            {
                gyms.Add(new BoxingGym(gymName));
            }
            if (gymType == "WeightliftingGym")
            {
                gyms.Add(new WeightliftingGym(gymName));
            }

            return $"Successfully added {gymType}.";

        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != "BoxingGloves" && equipmentType != "Kettlebell")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            if (equipmentType == "BoxingGloves")
            {
                equipment.Add(new BoxingGloves());
            }
            if (equipmentType == "Kettlebell")
            {
                equipment.Add(new Kettlebell());
            }
            return $"Successfully added {equipmentType}.";
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            if (equipment.FindByType(equipmentType) == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }

            IGym gym = gyms.Find(x => x.Name == gymName);
            IEquipment equip = this.equipment.FindByType(equipmentType);
            gym.AddEquipment(equip);
            equipment.Remove(equip);

            return $"Successfully added {equipmentType} to {gymName}.";

        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {

            if (athleteType != "Boxer" && athleteType != "Weightlifter")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            IGym gym = gyms.First(x => x.Name == gymName);
            IAthlete athlete = null;
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
                if (gym.GetType().Name != "BoxingGym")
                {
                    return "The gym is not appropriate.";
                }
            }
            if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                if (gym.GetType().Name != "WeightliftingGym")
                {
                    return "The gym is not appropriate.";
                }
            }
            gym.AddAthlete(athlete);
            return $"Successfully added {athleteType} to {gymName}.";
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.First(x => x.Name == gymName);
            foreach (IAthlete athlete in gym.Athletes)
            {
                athlete.Exercise();
            }
            return $"Exercise athletes: {gym.Athletes.Count}.";
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.First(x => x.Name == gymName);
            double totalEquipmentWeight = 0;
            foreach (IEquipment equip in gym.Equipment)
            {
                totalEquipmentWeight += equip.Weight;
            }

            return $"The total weight of the equipment in the gym {gymName} is {totalEquipmentWeight:f2} grams.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IGym gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();


        }
    }
}
