using System;
using System.Collections.Generic;
using System.Text;

namespace T01Bakery.Models.Drinks
{
    public class Tea: Drink
    {
        public Tea(string name, int portion, string brand) : base(name, portion, 2.50m, brand)
        {
        }
    }
}
