using NUnit.Framework;

namespace Robots.Tests
{
    using System;
    [TestFixture]
    public class RobotsTests
    {
        private RobotManager robotManager;
        private Robot robot;
        [SetUp]

        public void SetUp()
        {
            robotManager = new RobotManager(9);
            robot = new Robot("Gosho", 15);
        }
        [Test]

        public void checkAddMethodAndRobotCtorWorkignProperly()
        {
            Robot robot2 = new Robot("Pesho", 50);
            Robot robot3 = new Robot("Maimun", 100);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            Assert.AreEqual(3, robotManager.Count);
        }
        [Test]

        public void Exception_CheckForInvalidCapacity()
        {

            Assert.Throws<ArgumentException>(() => robotManager = new RobotManager(-3), "Invalid capacity!");
        }
        [Test]

        public void CheckForValidCapacity()
        {
            robotManager.Add(robot);
            Assert.AreEqual(9, robotManager.Capacity);
        }
        [Test]
        public void Exception_CheckAddMethodForExistingNameOfRobot()
        {
            Robot robot2 = new Robot("Pesho", 50);
            Robot robot3 = new Robot("Gosho", 100);
            robotManager.Add(robot);
            robotManager.Add(robot2);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot3),
                $"There is already a robot with name {robot3.Name}!");
        }
        [Test]
        public void Exception_CheckAddMethodWhenRobotManagerCapacityAlreadyReached()
        {
            for (int i = 0; i < 9; i++)
            {
                robotManager.Add(new Robot($"Pesho + {i}", 89 + i));
            }

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(new Robot("aaa", 20)),
                "Not enough capacity!");
        }
        [Test]
        public void Exception_CheckRemoveMethodWhenRobotWithThatNameDoesntExist()
        {
            for (int i = 0; i < 9; i++)
            {
                robotManager.Add(new Robot($"Pesho + {i}", 89 + i));
            }

            Assert.Throws<InvalidOperationException>(() => robotManager.Remove("aaa"),
                $"Robot with the name aaa doesn't exist!");
        }
        [Test]
        public void CheckRemoveMethodWorkingProperly()
        {
            for (int i = 0; i < 9; i++)
            {
                robotManager.Add(new Robot($"Pesho + {i}", 89 + i));
            }
            robotManager.Remove("Pesho + 0");
            Assert.AreEqual(8, robotManager.Count);

        }
        [Test]
        public void Exception_CheckWorkMethodWhenRobotWithThatNameDoesntExists()
        {
            for (int i = 0; i < 9; i++)
            {
                robotManager.Add(new Robot($"Pesho + {i}", 89 + i));
            }

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Pesho", "myn myn", 89), $"Robot with the name Pesho doesn't exist!");
        }
        [Test]
        public void Exception_CheckWorkMethodWhenRobotBatterIsLowerThanNecessaryBatteryUsage()
        {
            for (int i = 0; i < 9; i++)
            {
                robotManager.Add(new Robot($"Pesho + {i}", 89 + i));
            }

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Pesho + 0", "myn myn", 100),
                $"Pesho + 0 doesn't have enough battery!");
        }
        [Test]
        public void CheckWorkMethodWhenRobotBatterIsHigherThanNecessaryBatteryUsage()
        {
            Robot robot2 = new Robot("Pesho", 90);
            Robot robot3 = new Robot("Maimun", 100);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Work("Pesho", "myn myn", 85);
            Assert.AreEqual(5, robot2.Battery);
        }
        [Test]
        public void Exception_CheckMethodChargeWhenRobotWithThatNameDoesntExist()
        {
            Robot robot2 = new Robot("Pesho", 90);
            Robot robot3 = new Robot("Maimun", 100);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            
            Assert.Throws<InvalidOperationException>(() => robotManager.Charge("aaaaa"),
                $"Robot with the name aaaaa doesn't exist!");
        }
        [Test]
        public void CheckMethodChargeWhetherChargesRobotBatteryToMaximum()
        {
            Robot robot2 = new Robot("Pesho", 90);
            Robot robot3 = new Robot("Maimun", 100);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Work("Gosho", "myn myn", 5);
            robotManager.Work("Gosho", "myn myn", 5);
            robotManager.Charge("Gosho");
          Assert.AreEqual(15, robot.Battery ); 
        }

    }
}
