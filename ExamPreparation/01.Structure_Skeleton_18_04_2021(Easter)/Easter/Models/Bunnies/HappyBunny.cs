﻿using System;
using System.Collections.Generic;
using System.Text;
using Easter.Models.Bunnies;

namespace Easter.Models
{
   public class HappyBunny : Bunny
    {
        public HappyBunny(string name) : base(name, 100)
        {
        }

        public override void Work()
        {
            Energy -= 10;
            //if (Energy < 0)
            //{
            //    Energy = 0;
            //}

        }
    }
}
