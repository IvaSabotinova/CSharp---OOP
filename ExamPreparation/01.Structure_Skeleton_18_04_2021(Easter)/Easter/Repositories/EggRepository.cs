using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> eggs;

        public EggRepository()
        {
            eggs = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models { get; }
        public void Add(IEgg model)
        {
            eggs.Add(model);
        }

        public bool Remove(IEgg model)
        {
            return eggs.Remove(model);
        }

        public IEgg FindByName(string name)
        {
            return eggs.FirstOrDefault(x => x.Name == name);
        }
    }
}
