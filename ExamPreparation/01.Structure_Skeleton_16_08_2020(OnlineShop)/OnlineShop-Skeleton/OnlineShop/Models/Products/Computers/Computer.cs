using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.ComputerModels
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        //private double overallPerformance;
        //private decimal price;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if (components.Count == 0)
                {
                    return base.OverallPerformance;
                }

                return base.OverallPerformance + components.Average(x => x.OverallPerformance);

            }

        }
        public override decimal Price
        {
            get { return base.Price + components.Sum(x => x.Price) + peripherals.Sum(x => x.Price); }
        }

        

        public IReadOnlyCollection<IComponent> Components => components;
        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals;
        public void AddComponent(IComponent component)
        {

            if (components.Any(x=>x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ExistingComponent, component.GetType().Name,
                    GetType().Name, Id));
            }
            components.Add(component);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (components.Count == 0 || components.All(x=>x.GetType().Name != componentType))

            {
                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }

            IComponent componentToRemove = components.Find(x => x.GetType().Name == componentType);
            components.Remove(componentToRemove);
            return componentToRemove;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name,
                    GetType().Name, Id));
            }
            peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (peripherals.Count == 0 || peripherals.All(x => x.GetType().Name != peripheralType))

            {
                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }

            IPeripheral peripheralToRemove = peripherals.Find(x => x.GetType().Name == peripheralType);
            peripherals.Remove(peripheralToRemove);
            return peripheralToRemove;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");
            foreach (IComponent component in components)
            {
                sb.AppendLine($"  {component}");
            }

            double averageOverallPerformance = peripherals.Count > 0 ? peripherals.Average(x => x.OverallPerformance) : 0;
            sb.AppendLine(
                $" Peripherals ({peripherals.Count}); Average Overall Performance ({averageOverallPerformance:f2}):");
            foreach (IPeripheral peripheral in peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
