﻿using System;
using System.Collections.Generic;
using System.Text;
using Gym.Utilities.Messages;

namespace Gym.Models.Athletes
{


    public class Boxer : Athlete
    {
        //Can train only in a BoxingGym.
        public Boxer(string fullName, string motivation, int numberOfMedals) : base(fullName, motivation,
            numberOfMedals, 60)
        {
        }

        public override void Exercise()
        {
           Stamina += 15;
            if (Stamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }

    }
}
