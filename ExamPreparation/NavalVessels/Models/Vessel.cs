using System;
using System.Collections.Generic;
using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double mainWeaponCaliber;
        private double armorThickness;
        private double speed;
        private  List<string> targets;


        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
           targets = new List<string>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(name),ExceptionMessages.InvalidVesselName);
                }

                name = value;
            }
        }

        public ICaptain Captain
        {
            get { return captain; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }

                captain = value;
            }
        }

        public double ArmorThickness
        {
            get { return armorThickness;}
            set { armorThickness = value; }
        }
        public double MainWeaponCaliber
        {
            get { return mainWeaponCaliber;}
            protected set
            {
                mainWeaponCaliber = value;
            }
        }
        public double Speed {
            get { return speed; }
            protected set
            {
                speed = value;
            }
        }
        public ICollection<string> Targets => targets;

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }

            target.ArmorThickness -= MainWeaponCaliber;
            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }

            Targets.Add(target.Name);
        }

        public virtual void RepairVessel()
        {
            ArmorThickness = default;
        }
       

        public override string ToString()
        {
            string result = Targets.Count == 0 ? "None" : string.Join(", ", Targets);
            return $"- {Name}" + Environment.NewLine +
                   $" *Type: {GetType().Name}" + Environment.NewLine +
                   $" *Armor thickness: {ArmorThickness}" + Environment.NewLine +
                   $" *Main weapon caliber: {MainWeaponCaliber}" + Environment.NewLine +
                   $" *Speed: {Speed} knots" + Environment.NewLine +
                   $" *Targets: {result}";
            }

    }
}
