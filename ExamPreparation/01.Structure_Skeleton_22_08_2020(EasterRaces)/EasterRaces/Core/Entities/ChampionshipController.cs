using System;
using System.Collections.Generic;
using System.Linq;
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository carRepository;
        private DriverRepository driverRepository;
        private RaceRepository raceRepository;
        public ChampionshipController()
        {
            carRepository = new CarRepository();
            driverRepository = new DriverRepository();
            raceRepository = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            if (driverRepository.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            driverRepository.Add(new Driver(driverName));
            return String.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (carRepository.GetByName(model) != null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CarExists, model));
            }

            ICar car = null;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);

            }
            if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            carRepository.Add(car);
            return $"{car.GetType().Name} {model} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = raceRepository.GetByName(name);
            if (race != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExists, name));
            }
            raceRepository.Add(new Race(name, laps));
            return String.Format(OutputMessages.RaceCreated, name);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = raceRepository.GetByName(raceName);
            IDriver driver = driverRepository.GetByName(driverName);
            if (race == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (driver == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            race.AddDriver(driver);
            return String.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = driverRepository.GetByName(driverName);
            ICar car = carRepository.GetByName(carModel);
            if (driver == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            if (car == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarNotFound, carModel));
            }
            driver.AddCar(car);
            return String.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            List<IDriver> sortedListOfDrivers =
                race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).ToList();
            raceRepository.Remove(race);
            return $"Driver {sortedListOfDrivers.ElementAt(0).Name} wins {raceName} race." + Environment.NewLine +
            $"Driver {sortedListOfDrivers.ElementAt(1).Name} is second in {raceName} race." + Environment.NewLine +
           $"Driver {sortedListOfDrivers.ElementAt(2).Name} is third in {raceName} race.";

        }
    }
}
