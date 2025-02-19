﻿using System;

using Bakery.Models.Drinks.Contracts;
using Bakery.Utilities.Messages;

namespace T01Bakery.Models.Drinks
{
    public abstract class Drink: IDrink
    {
        private string name;
        private int portion;
        private decimal price;
        private string brand;
        protected Drink(string name, int portion, decimal price, string brand)
        {
            Name = name;
            Portion = portion;
            Price = price;
            Brand = brand;
        }
        
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidName);
                }
                name = value;
            }
        }
        public int Portion
        {
            get { return portion; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPortion);
                }
                portion = value;
            }
        }
        public decimal Price
        {
            get { return price; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPortion);
                }
                price = value;
            }
        }
        public string Brand
        {
            get { return brand; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBrand);
                }
                brand = value;
            }
        }

        public string ToString()
        {
            return $"{Name} {Brand} - {Portion}ml - {Price:f2}lv";
        }
    }
}
