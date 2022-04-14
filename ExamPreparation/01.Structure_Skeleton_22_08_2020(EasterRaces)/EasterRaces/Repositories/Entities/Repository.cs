using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository <T>: IRepository<T>
    {
        private List<T> getAll;

        protected Repository()
        {
            getAll = new List<T>();
        }

        public abstract T GetByName(string name);
      

        public IReadOnlyCollection<T> GetAll() => getAll;
       

        public void Add(T model)
        {
            getAll.Add(model);
        }

        public bool Remove(T model)
        {
            return getAll.Remove(model);
        }
    }
}
