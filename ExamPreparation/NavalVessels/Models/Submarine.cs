using System;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private bool submergeMode;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 200)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode
        {
            get { return submergeMode; }
            private set
            {
                submergeMode = value;
            }
        }
        public void ToggleSubmergeMode()
        {
            if (!SubmergeMode)
            {
                MainWeaponCaliber += 40;

                Speed -= 4;
                SubmergeMode = true;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
                SubmergeMode = false;
            }
        }
        public override void RepairVessel()
        {
            ArmorThickness = 200;
        }
        public override string ToString()
        {
            string result = SubmergeMode ? "ON" : "OFF";
            return base.ToString() + Environment.NewLine +
                   $" *Submerge mode: {result}";
        }
    }
}
