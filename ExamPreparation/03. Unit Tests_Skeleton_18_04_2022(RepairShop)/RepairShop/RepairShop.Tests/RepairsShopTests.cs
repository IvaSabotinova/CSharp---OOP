using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        private Car car;
        private Garage garage;

        [SetUp]
        public void SetUp()
        {
            car = new Car("aaa", 2);
            garage = new Garage("garage1", 3);

        }

        [Test]
        public void CheckObjectsExist()
        {
            Assert.IsNotNull(car);
            Assert.IsNotNull(garage);
            Assert.AreEqual(0, garage.CarsInGarage);

        }
        [Test]
        [TestCase(null, 2)]
        [TestCase("", 1)]
        public void Exception_CheckCtorOfGarageForInvalidName(string name, int mechanicsAvailable)
        {
            Assert.Throws<ArgumentNullException>(() => new Garage(name, mechanicsAvailable), nameof(name), "Invalid garage name.");

        }

        [Test]
        //[TestCase("aaa", 0)]
        [TestCase("bbb", -1)]
        public void Exception_CheckCtorOfGarageForInvalidNumberOfMechanics(string name, int mechanicsAvailable)
        {
            Assert.Throws<ArgumentException>(() => new Garage(name, mechanicsAvailable), "At least one mechanic must work in the garage.");

        }

        [Test]
        [TestCase("ccc", 5)]
        [TestCase("ddd", 6)]
        public void CheckCtorOfGarageForValidProperties(string name, int mechanicsAvailable)
        {
            garage = new Garage(name, mechanicsAvailable);
            Assert.AreEqual(name, garage.Name);
            Assert.AreEqual(mechanicsAvailable, garage.MechanicsAvailable);
            Assert.IsNotNull(garage);

        }
        [Test]
        public void Exception_CheckAddMethodWhenNotEnoughMechanics()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.Throws<InvalidOperationException>(() => garage.AddCar(new Car("eee", 5)), "No mechanic available.");

        }
        [Test]
        public void CheckAddMethodAndCarsInGarageProperty()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.AreEqual(3, garage.CarsInGarage);

        }
        [Test]
        public void Exception_CheckMethodFixCar()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.Throws<InvalidOperationException>(() => garage.FixCar("ddd"), "The car ddd doesn't exist.");

        }
        [Test]
        public void CheckMethodFixCar()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.AreEqual(car, garage.FixCar("aaa"));
            Assert.AreEqual(0, car.NumberOfIssues);

        }
        [Test]
        public void Exception_CheckMethodRemoveFixedCar()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar(), "No fixed cars available.");

        }
        [Test]
        public void CheckMethodRemoveFixedCar()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            garage.FixCar("ccc");
            Assert.AreEqual(0, car3.NumberOfIssues);
            Assert.AreEqual(1, garage.RemoveFixedCar());

        }
        [Test]
        public void CheckMethodReport()
        {
            Car car2 = new Car("bbb", 1);
            Car car3 = new Car("ccc", 3);
            garage.AddCar(car);
            garage.AddCar(car2);
            garage.AddCar(car3);
            Assert.AreEqual("There are 3 which are not fixed: aaa, bbb, ccc.", garage.Report());
        }

    }
}