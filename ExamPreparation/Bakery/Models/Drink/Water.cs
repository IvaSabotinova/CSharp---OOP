using System;
using System.Collections.Generic;
using System.Text;

namespace T01Bakery.Models.Drinks
{
    public class Water: Drink
    {
        public Water(string name, int portion, string brand) : base(name, portion, 1.50m, brand)
        {
        }
    }
}
