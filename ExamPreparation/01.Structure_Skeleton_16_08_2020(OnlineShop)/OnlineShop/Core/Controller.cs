using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.ComputerModels;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.PeriferalsModels;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private Dictionary<int, IComputer> Id_Computers;
        private List<IComponent> components = new List<IComponent>();
        private List<IPeripheral> peripherals = new List<IPeripheral>();

        public Controller()
        {
            Id_Computers = new Dictionary<int, IComputer>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (Id_Computers.ContainsKey(id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            if (computerType != nameof(DesktopComputer) && computerType != nameof(Laptop))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            IComputer computer = null;
            if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            Id_Computers.Add(id, computer);

            return $"Computer with id {id} added successfully.";
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            if (!Id_Computers.ContainsKey(computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            if (Id_Computers[computerId].Peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }


            IPeripheral peripheral = null;
            if (peripheralType == nameof(Headset))
            {
                peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Keyboard))
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Monitor))
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Mouse))
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            Id_Computers[computerId].AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return String.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);


        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            if (!Id_Computers.ContainsKey(computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = Id_Computers[computerId].RemovePeripheral(peripheralType);
            peripherals.Remove(peripheral);
            return String.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
            if (!Id_Computers.ContainsKey(computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            if (components.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }


            IComponent component = null;
            if (componentType == nameof(CentralProcessingUnit))
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance,
                    generation);
            }
            else if (componentType == nameof(Motherboard))
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(PowerSupply))
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(RandomAccessMemory))
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(SolidStateDrive))
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(VideoCard))
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            Id_Computers[computerId].AddComponent(component);
            components.Add(component);

            return String.Format(SuccessMessages.AddedComponent, componentType, id, computerId);

        }

        public string RemoveComponent(string componentType, int computerId)
        {
            if (!Id_Computers.ContainsKey(computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            
            Id_Computers[computerId].RemoveComponent(componentType);
            IComponent componentToRemove = components.FirstOrDefault(x => x.GetType().Name == componentType);
            components.Remove(componentToRemove);
            
            return String.Format(SuccessMessages.RemovedComponent, componentType, componentToRemove.Id);
        }

        public string BuyComputer(int id)
        {
            if (!Id_Computers.ContainsKey(id))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            string result = Id_Computers[id].ToString();
            Id_Computers.Remove(id);
            return result;
        }

        public string BuyBest(decimal budget)
        {
            IComputer computer = Id_Computers.Values.OrderByDescending(x => x.OverallPerformance)
                 .Where(x => x.Price <= budget).FirstOrDefault();
            if (computer == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            Id_Computers.Remove(computer.Id);
            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            if (!Id_Computers.ContainsKey(id))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            return Id_Computers[id].ToString();
        }
    }

}
