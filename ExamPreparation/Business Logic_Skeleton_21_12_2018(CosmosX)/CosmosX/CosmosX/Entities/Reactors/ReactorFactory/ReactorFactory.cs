using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Reactors.Contracts;
using CosmosX.Entities.Reactors.ReactorFactory.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace CosmosX.Entities.Reactors.ReactorFactory
{
    public class ReactorFactory : IReactorFactory
    {

        public IReactor CreateReactor(string reactorTypeName, int id, IContainer moduleContainer, int additionalParameter)
        {
           
            Type type = Assembly.GetCallingAssembly().GetTypes().First(x => x.Name.StartsWith(reactorTypeName));

            IReactor reactor = Activator.CreateInstance(type, id, moduleContainer, additionalParameter) as IReactor;
            return reactor;
        }
   
    }
}
