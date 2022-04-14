using System;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
   public class Map :IMap
    {
      public string StartRace(IRacer racerOne, IRacer racerTwo)
            {


            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return String.Format(OutputMessages.RaceCannotBeCompleted);
            }
            if (!racerOne.IsAvailable())
            {
                return String.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            if (!racerTwo.IsAvailable())
            {
                return String.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            IRacer racer1 = null;
            IRacer racer2 = null;
            if (racerOne.GetType().Name == nameof(ProfessionalRacer))
            {
                racer1 = racerOne as ProfessionalRacer;

            }
            else if (racerOne.GetType().Name == nameof(StreetRacer))
            {
                racer1 = racerOne as StreetRacer;

            }
            if (racerTwo.GetType().Name == nameof(ProfessionalRacer))
            {
                racer2 = racerTwo as ProfessionalRacer;

            }
            else if (racerTwo.GetType().Name == nameof(StreetRacer))
            {
                racer2 = racerTwo as StreetRacer;

            }
            racer1.Race();
            racer2.Race();
            double racer1Multiplier = racer1.RacingBehavior == "strict" ? 1.2 : 1.1;
            double racer2Multiplier = racer2.RacingBehavior == "strict" ? 1.2 : 1.1;

            double scoresRacer1 = racer1.Car.HorsePower * racer1.DrivingExperience * racer1Multiplier;
            double scoresRacer2 = racer2.Car.HorsePower * racer2.DrivingExperience * racer2Multiplier;
            IRacer winner = scoresRacer1 > scoresRacer2 ? racerOne : racerTwo;
            return String.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);

        }
    }
}
