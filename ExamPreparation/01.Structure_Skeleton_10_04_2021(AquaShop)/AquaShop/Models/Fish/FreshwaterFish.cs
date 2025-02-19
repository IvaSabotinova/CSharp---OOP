﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
   public class FreshwaterFish: Fish
   {
      
        public FreshwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
            Size = 3;
        }
        //Can only live in FreshwaterAquarium!
        public override void Eat()
        {
            Size += 3;
        }
    }
}
