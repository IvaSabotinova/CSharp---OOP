using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {

        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private int countOfExploredPlanets;

        public Controller()
        {
            astronautRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
            countOfExploredPlanets = 0;

        }

        public string AddAstronaut(string type, string astronautName)
        {

            if (type == nameof(Biologist))
            {
                astronautRepository.Add(new Biologist(astronautName));
            }
            else if (type == nameof(Geodesist))
            {
                astronautRepository.Add(new Geodesist(astronautName));
            }
            else if (type == nameof(Meteorologist))
            {
                astronautRepository.Add(new Meteorologist(astronautName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
            return String.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            for (int i = 0; i < items.Length; i++)
            {
                planet.Items.Add(items[i]);
            }
            planetRepository.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }
        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronautRepository.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRetiredAstronaut,
                    astronautName));
            }

            astronautRepository.Remove(astronaut);
            return String.Format(OutputMessages.AstronautRetired, astronautName);

        }
        
        public string ExplorePlanet(string planetName)
        {
            IMission mission = new Mission();
            IPlanet planet = planetRepository.FindByName(planetName);
            List<IAstronaut> astronauts = new List<IAstronaut>();
            foreach (IAstronaut astr in astronautRepository.Models)
            {
                if (astr.Oxygen > 60)
                {
                    astronauts.Add(astr);
                }

            }

            int initialCountOfAstronauts = astronauts.Count;

            if (astronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }
            mission.Explore(planet, astronauts);
            countOfExploredPlanets++;
            return String.Format(OutputMessages.PlanetExplored, planetName, initialCountOfAstronauts - astronauts.Count);

        }

        public string Report()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{countOfExploredPlanets} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (IAstronaut astron in astronautRepository.Models)
            {
                string bagResult = astron.Bag.Items.Count > 0 ? string.Join(", ", astron.Bag.Items) : "none";
                sb.AppendLine($"Name: {astron.Name}");
                sb.AppendLine($"Oxygen: {astron.Oxygen}");
                sb.AppendLine($"Bag items: {bagResult}");
            }

            return sb.ToString().TrimEnd();
            
        }
    }
}
