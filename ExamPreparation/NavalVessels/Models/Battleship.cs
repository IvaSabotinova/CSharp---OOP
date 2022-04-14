using System;
using NavalVessels.Models.Contracts;


namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private bool sonarMode;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
        {
            SonarMode = false;
        }

        public bool SonarMode
        {
            get { return sonarMode; }
            private set
            {
                sonarMode = value;
            }
        }
        public override void RepairVessel()
        {
            ArmorThickness = 300;
        }

        public void ToggleSonarMode()
        {
            if (!SonarMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
                SonarMode = true;
            }

            else
            {
                
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
                SonarMode = false;
            }
        }

        public override string ToString()
        {
            string result = SonarMode ? "ON" : "OFF";
            return base.ToString() + Environment.NewLine +
                   $" *Sonar mode: {result}";
        }
    }
}
