using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRacing.Core.Contracts;
using CarRacing.Models;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;

        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type == nameof(SuperCar))
            {
                cars.Add(new SuperCar(make, model, VIN, horsePower));
            }
            else if (type == nameof(TunedCar))
            {
                cars.Add(new TunedCar(make, model, VIN, horsePower));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            return String.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            if (cars.FindBy(carVIN) == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            if (type != nameof(ProfessionalRacer) && type != nameof(StreetRacer))
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            if (type == nameof(ProfessionalRacer))
            {
                racers.Add(new ProfessionalRacer(username, cars.FindBy(carVIN)));
            }
            if (type == nameof(StreetRacer))
            {
                racers.Add(new StreetRacer(username, cars.FindBy(carVIN)));
            }
            return String.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IMap map = new Map();
            IRacer racerOne = racers.FindBy(racerOneUsername);
            IRacer racerTwo = racers.FindBy(racerTwoUsername);
            if (racerOne == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }
            if (racerTwo == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }
            string result = map.StartRace(racerOne, racerTwo);

            return result;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            List<IRacer> racersResult = racers.Models.ToList().OrderByDescending(x => x.DrivingExperience)
                .ThenBy(x => x.Username).ToList();
            foreach (IRacer racer in racersResult)
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
