﻿using System;
using System.Collections.Generic;
using System.Text;
using Gym.Utilities.Messages;

namespace Gym.Models.Athletes
{

    public class Weightlifter : Athlete
    {
        //Can train only in a WeightliftingGym.
        public Weightlifter(string fullName, string motivation, int numberOfMedals) : base(fullName, motivation, numberOfMedals, 50)
        {
        }

        public override void Exercise()
        {
            Stamina += 10;
            if (Stamina > 100)
            {
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
