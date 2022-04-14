using System;
using System.Collections.Generic;
using System.Text;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars
{
    public class MuscleCar : Car
    {
        public MuscleCar(string model, int horsePower) : base(model, horsePower, 5000, 400, 600)
        {
            if (horsePower < 400 || horsePower > 600)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, horsePower));
            }

        }
        public override double CalculateRacePoints(int laps)
        {
            return 5000 / HorsePower * laps;
        }
    }
}
