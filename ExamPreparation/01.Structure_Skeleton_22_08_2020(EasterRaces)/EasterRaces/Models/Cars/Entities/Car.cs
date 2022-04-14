using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars.Entities
{
   public abstract class Car: ICar
   {
       private string model;
       private int horsePower;
       private double cubicCentimeters;
       //private int minHorsePower;
       //private int maxHorsePower;

       protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
       {
           Model = model;
           HorsePower = horsePower;
           CubicCentimeters = cubicCentimeters;
            
       }

        public string Model
        {
            get
            {
                return model;
            } 
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidModel, value, 4));
                }

                model = value;
            }

        }
        public int HorsePower
        {
            get { return horsePower;}
            private set
            {
                horsePower = value;
            }
        }
        public double CubicCentimeters
        {
            get { return cubicCentimeters; }
            private set { cubicCentimeters = value; }
        }

        public abstract double CalculateRacePoints(int laps);

   }
}
