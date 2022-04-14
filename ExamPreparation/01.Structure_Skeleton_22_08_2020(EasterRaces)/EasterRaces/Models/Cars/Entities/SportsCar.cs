using System;
using System.Collections.Generic;
using System.Text;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        public SportsCar(string model, int horsePower) : base(model, horsePower, 3000, 250, 450)
        {
            if (horsePower < 250 || horsePower > 450)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, horsePower));
            }
        }

        public override double CalculateRacePoints(int laps)
        {
            return 3000 / HorsePower * laps;
        }
    }
}
