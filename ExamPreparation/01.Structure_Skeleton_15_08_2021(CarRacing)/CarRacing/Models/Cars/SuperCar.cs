using System;
using System.Collections.Generic;
using System.Text;
using CarRacing.Models.Cars;

namespace CarRacing.Models
{
    public class SuperCar : Car
    {

        public SuperCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, 80,
            10)
        {
        }
    }
}

