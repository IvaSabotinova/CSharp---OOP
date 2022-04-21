using SoftUniDI.Attributes;
using SoftUniDI.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftUniDI.Modules
{

    public abstract class AbstractModule : IModule
    {
        private  IDictionary<Type, Dictionary<string, Type>> implementations;
        private  IDictionary<Type, object> instances;

        protected AbstractModule()
        {
            implementations = new Dictionary<Type, Dictionary<string, Type>>();
            instances = new Dictionary<Type, object>();
        }

        public abstract void Configure();

        protected void CreateMapping<TInter, TImpl>()
        {
            if (!implementations.ContainsKey(typeof(TInter)))
            {
                implementations[typeof(TInter)] = new Dictionary<string, Type>();
            }
            implementations[typeof(TInter)].Add(typeof(TImpl).Name, typeof(TImpl));

        }
        public Type GetMapping(Type currentInterface, object attribute)
        {
            Dictionary<string, Type> currentImplementation = implementations[currentInterface];
            Type type = null;
            if (attribute is Inject)
            {
                if (currentImplementation.Count == 1)
                {
                    type = currentImplementation.Values.First();
                }
                else
                {
                    throw new ArgumentException("No available mapping for class: " + currentInterface.FullName);
                }
            }
            else if (attribute is Named)
            {
                Named named = attribute as Named;
                string dependencyName = named.Name;
                type = currentImplementation[dependencyName];
            }

            return type;
        }

        public object GetInstance(Type implementation)
        {
            instances.TryGetValue(implementation, out object value);
            return value;
        }

        public void SetInstance(Type implementation, object instance)
        {
            if (!instances.ContainsKey(implementation))
            {
                instances.Add(implementation, instance);
            }
        }
    }
}
