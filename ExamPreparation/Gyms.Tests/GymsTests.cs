using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Gyms.Tests
{
    [TestFixture]
    public class GymsTests
    {
        private Gym gym;
        private Athlete athlete;
        [SetUp]
        public void SetUp()
        {
            gym = new Gym("aaa", 9);
            athlete = new Athlete("PeshoG");
        }

        [Test]
        [TestCase(null, 9)]
        [TestCase("", 9)]
        public void Exception_CheckCtorName(string name, int capacity)
        {
            Assert.Throws<ArgumentNullException>((() => gym = new Gym(name, capacity)), "Invalid gym name.");

        }

        [TestCase("aaa", -1)]
        public void Exception_CheckCtorCapacity(string name, int capacity)
        {
            Assert.Throws<ArgumentException>((() => gym = new Gym(name, capacity)), "Invalid gym capacity.");
        }

        [Test]
        public void Exception_CheckMethodAddAthlete()
        {
            gym.AddAthlete(athlete);
            for (int i = 0; i < 8; i++)
            {
                gym.AddAthlete(new Athlete($"Goshkata{i}"));
            }

            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(new Athlete("maimun")), "The gym is full.");
        }

        [Test]
        public void CheckMethodAddAthlete()
        {
            gym.AddAthlete(athlete);
            for (int i = 0; i < 5; i++)
            {
                gym.AddAthlete(new Athlete($"Pesho{i}"));
            }
            Assert.AreEqual(6, gym.Count);
        }
        [Test]
        public void Exception_CheckMethodRemoveAthlete()
        {
            gym.AddAthlete(athlete);
            for (int i = 0; i < 8; i++)
            {
                gym.AddAthlete(new Athlete($"Goshkata{i}"));
            }

            string fullName = "maimun";
            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete(fullName), $"The athlete {fullName} doesn't exist.");
        }

        [Test]
        public void CheckMethodRemoveAthlete()
        {
            gym.AddAthlete(athlete);
            for (int i = 0; i < 5; i++)
            {
                gym.AddAthlete(new Athlete($"Pesho{i}"));
            }
            gym.RemoveAthlete("PeshoG");
            Assert.AreEqual(5, gym.Count);
        }
        [Test]
        public void Exception_CheckMethodInjureAthlete()
        {
            gym.AddAthlete(athlete);
          string fullName = "maimun";
            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete(fullName), $"The athlete {fullName} doesn't exist.");
        }

        [Test]
        public void CheckMethodInjureAthlete()
        {
            gym.AddAthlete(athlete);
            //gym.InjureAthlete("PeshoG");
            //Assert.IsTrue(athlete.IsInjured);
            Assert.AreEqual(athlete,gym.InjureAthlete("PeshoG"));
       
        }
        [Test]
        public void CheckReportMethod()
        {
            gym.AddAthlete(athlete);
            gym.AddAthlete(new Athlete("aaa"));
            gym.AddAthlete(new Athlete("bbb"));
            Assert.AreEqual($"Active athletes at {gym.Name}: {athlete.FullName}, {"aaa"}, {"bbb"}", gym.Report());
        }
    }
}
