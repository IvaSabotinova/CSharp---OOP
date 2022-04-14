using System;
using System.Collections.Generic;

using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;

using Bakery.Utilities.Messages;

namespace T01Bakery.Models.Tables
{

    public abstract class Table : ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;
        private decimal totalTableBill;
        protected Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }

        public int TableNumber { get; }
        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                capacity = value;
            }
        }
        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                numberOfPeople = value;
            }

        }
        public decimal PricePerPerson { get; private set; }
        public bool IsReserved { get; private set; }
        public decimal Price => NumberOfPeople * PricePerPerson;
        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            this.numberOfPeople = numberOfPeople;
        }

        public void OrderFood(IBakedFood food)
        {
            foodOrders.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public decimal GetBill()
        {

            foreach (IBakedFood food in foodOrders)
            {
                totalTableBill += food.Price;
            }

            foreach (IDrink drink in drinkOrders)
            {
                totalTableBill += drink.Price;
            }

            totalTableBill += Price;
            return totalTableBill;
        }

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();
            totalTableBill = 0;
            IsReserved = false;
        }

        public string GetFreeTableInfo()
        {
            return $"Table: {TableNumber}" + Environment.NewLine +
            $"Type: {GetType().Name}" + Environment.NewLine +
            $"Capacity: {Capacity}" + Environment.NewLine +
            $"Price per Person: {PricePerPerson}";
        }
    }
}
