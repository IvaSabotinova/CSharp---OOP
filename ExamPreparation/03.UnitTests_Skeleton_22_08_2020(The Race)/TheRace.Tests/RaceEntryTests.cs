using System;
using NUnit.Framework;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private UnitCar unitCar;
        private UnitDriver unitDriver;
        private RaceEntry raceEntry;
        [SetUp]
        public void Setup()
        {
            unitCar = new UnitCar("Mazda", 150, 50);
            unitDriver = new UnitDriver("Pesho", unitCar);
            raceEntry = new RaceEntry();
        }

        [Test]
        public void Exception_CheckAddMethodWhenDriverIsNull()
        {
            unitDriver = null;
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver), "Driver cannot be null.");
        }
        [Test]
        public void Exception_CheckAddMethodWhenDriverIsAlreadyAdded()
        {
            raceEntry.AddDriver(unitDriver);
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver), $"Driver {unitDriver.Name} is already added.");
        }
        [Test]
        public void CheckAddMethodWhenAddingNewDriver()
        {
            UnitCar unitCar2 = new UnitCar("Pegeuot", 25, 8);
            UnitDriver unitDriver2 = new UnitDriver("Gosho", unitCar2);
            raceEntry.AddDriver(unitDriver);
           Assert.AreEqual(1, raceEntry.Counter);
           Assert.AreEqual($"Driver {unitDriver2.Name} added in race.", raceEntry.AddDriver(unitDriver2));
        }
        [Test]
        public void Exception_CheckMethodCalculateAverageHorsePower()
        {
            raceEntry.AddDriver(unitDriver);
            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower(), $"The race cannot start with less than {raceEntry.Counter} participants.");
        }
        [Test]
        public void CheckMethodCalculateAverageHorsePower()
        {
            UnitCar unitCar2 = new UnitCar("Pegeuot", 25, 8);
            UnitCar unitCar3 = new UnitCar("Mercedes", 50, 10);
            UnitDriver unitDriver2 = new UnitDriver("Gosho", unitCar2);
            UnitDriver unitDriver3 = new UnitDriver("Maimun", unitCar3);
            raceEntry.AddDriver(unitDriver);
            raceEntry.AddDriver(unitDriver2);
            raceEntry.AddDriver(unitDriver3);
         Assert.AreEqual(75, raceEntry.CalculateAverageHorsePower());
        }
    }
}
