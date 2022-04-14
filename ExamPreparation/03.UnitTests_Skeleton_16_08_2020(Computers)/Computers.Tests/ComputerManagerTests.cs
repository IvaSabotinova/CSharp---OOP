using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Computers.Tests
{
    public class Tests
    {
        private ComputerManager computerManager;
        private Computer computer;
        [SetUp]
        public void Setup()
        {
            computer = new Computer("Aaa", "Pht", 25);
            computerManager = new ComputerManager();
        }

        [Test]
        public void CheckCtor()
        {
            Assert.IsNotNull(computer);
            Assert.IsNotNull(computerManager.Computers);
        }
        [Test]
        public void Exception_CheckAddMethodValidateNullValue()
        {
            computer = null;
            Assert.Throws<ArgumentNullException>(() => computerManager.AddComputer(computer), "Can not be null!");
        }
        [Test]
        public void Exception_CheckAddMethodWhenComputerAlreadyExists()
        {
            computerManager.AddComputer(computer);
           Assert.Throws<ArgumentException>(() => computerManager.AddComputer(computer), "This computer already exists.");
        }
        [Test]
        public void CheckAddMethodWhenWhenAddingInexistentComputer()
        {
            Computer computer2 = new Computer("bbb", "tct", 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            Assert.AreEqual(3, computerManager.Count);
        }
        [Test]
        public void Exception_CheckMethodGetComputerValidateNullManufacturerValue()
        {
            computer = new Computer(null, "bbb", 25);
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputer(computer.Manufacturer, computer.Model), "Can not be null!");
        }
        [Test]
        public void Exception_CheckMethodGetComputerValidateNullModelValue()
        {
            computer = new Computer("TCT", null, 25);
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputer(computer.Manufacturer, computer.Model), "Can not be null!");
        }
        [Test]
        public void Exception_CheckMethodGetComputerWhenThereIsNotComputerWithThisManufacturerAndName()
        {
            Computer computer2 = new Computer("bbb", "tct", 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            Assert.Throws<ArgumentException>(() => computerManager.GetComputer("ddd", "Python"), "There is no computer with this manufacturer and model.");
        }
        [Test]
        public void CheckMethodGetComputerWhenThereIsComputerWithThisManufacturerAndName()
        {
            Computer computer2 = new Computer("bbb", "tct", 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            Assert.AreEqual(computer, computerManager.GetComputer("Aaa", "Pht"));
        }
        [Test]
        public void Exception_CheckMethodRemoveComputerWhenWeHaveNullManufacturer()
        {
            Computer computer2 = new Computer(null, "tct", 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            Assert.Throws<ArgumentNullException>(() => computerManager.RemoveComputer(null, "tct"), "Can not be null!");
        }
        [Test]
        public void Exception_CheckMethodRemoveComputerWhenWeHaveNullModel()
        {
            Computer computer2 = new Computer("bbb", null, 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            Assert.Throws<ArgumentNullException>(() => computerManager.RemoveComputer("bbb", null), "Can not be null!");
        }
        [Test]
        public void CheckMethodRemoveComputer()
        {
            Computer computer2 = new Computer("bbb", "tct", 50);
            Computer computer3 = new Computer("ccc", "gpg", 75);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
           Assert.AreEqual(computer, computerManager.RemoveComputer("Aaa", "Pht"));
        }
        [Test]
        public void Exception_CheckMethodGetComputersByManufacturerValidateNullValue()
        {
            Computer testComputer = new Computer(null, "pghth", 35);
        computerManager.AddComputer(testComputer);
        Assert.Throws<ArgumentNullException>(() => computerManager.GetComputersByManufacturer(testComputer.Manufacturer), "Can not be null!");
        }
        [Test]
        public void CheckMethodGetComputersByManufacturer()
        {
            List<Computer> testOne = new List<Computer>();
            Computer computer2 = new Computer("bbb", "tct", 50);
            Computer computer3 = new Computer("ccc", "fff", 75);
            Computer computer4 = new Computer("ccc", "ggg", 65);
            Computer computer5 = new Computer("ccc", "ttt", 150);
            computerManager.AddComputer(computer);
            computerManager.AddComputer(computer2);
            computerManager.AddComputer(computer3);
            computerManager.AddComputer(computer4);
            computerManager.AddComputer(computer5);
          testOne.Add(computer3);
            testOne.Add(computer4);
            testOne.Add(computer5);
            Assert.AreEqual(testOne, computerManager.GetComputersByManufacturer("ccc"));
        }
    }
}