using System;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Models.Planets;
using System.Linq;
using SpaceStation.Models.Planets.Contracts;


namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;
        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planets;

        public void Add(IPlanet model)
        {
            planets.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return planets.Find(x => x.Name == name);
        }

        public bool Remove(IPlanet model)
        {
            return planets.Remove(model);

        }
    }
}
