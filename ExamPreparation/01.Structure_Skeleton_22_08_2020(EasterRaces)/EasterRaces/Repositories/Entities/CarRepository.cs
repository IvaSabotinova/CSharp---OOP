using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Models.Cars.Contracts;

namespace EasterRaces.Repositories.Entities
{
   public class CarRepository : Repository<ICar>
    {
        public override ICar GetByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Model == name);
        }
    }
}
