using System.Reflection;
using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;
    [TestFixture]
    public class AquariumsTests
    {
        private Fish fish;
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            fish = new Fish("aaa");
            aquarium = new Aquarium("aq1", 20);
        }

        [Test]
        public void CheckCtor()
        {
            Assert.IsNotNull(fish);
            Assert.IsNotNull(aquarium);
        }

        [TestCase(null, 5)]
        [TestCase("", 5)]
        public void Exceptions_CheckCtorWhenNameIsInvalid(string name, int capacity)
        {
            Assert.Throws<ArgumentNullException>(() => aquarium = new Aquarium(name, capacity),
                "Invalid aquarium name!");
        }
        [TestCase("aaa", -5)]
      public void Exceptions_CheckCtorWhenCapacityIsInvalid(string name, int capacity)
        {
            Assert.Throws<ArgumentException>(() => aquarium = new Aquarium(name, capacity),
                "Invalid aquarium capacity!");
        }
      [Test]
      public void Exception_CheckAddMethodWhenCapacityIsReached()
      {
          for (int i = 0; i < 20; i++)
          {
           aquarium.Add(new Fish($"aaa+{i}"));
          }

          Assert.Throws<InvalidOperationException>((() => aquarium.Add(fish)), "Aquarium is full!");

      }
        [Test]
      public void CheckCtorWhenCreatingAquarium()
      {
          Fish fish2 = new Fish("bbb");
          Fish fish3 = new Fish("ccc");
          aquarium.Add(fish);
          aquarium.Add(fish2);
          aquarium.Add(fish3);
          Assert.AreEqual(3, aquarium.Count);

      }
      [Test]
      public void Exception_CheckRemoveMethodWhenFishDoesntExist()
      {
          for (int i = 0; i < 20; i++)
          {
              aquarium.Add(new Fish($"aaa+{i}"));
          }
          
          Assert.Throws<InvalidOperationException>((() => aquarium.RemoveFish("aaa")), "Fish with the name aaa doesn't exist!");
            }
      [Test]
      public void CheckRemoveMethodWhenFishExists()
      {
          for (int i = 0; i < 17; i++)
          {
              aquarium.Add(new Fish($"aaa+{i}"));
          }
          aquarium.Add(fish);
          aquarium.RemoveFish(fish.Name);
         Assert.AreEqual(17, aquarium.Count);
      }
      [Test]
      public void Exception_CheckSellFishMethodWhenFishDoesntExist()
      {
          for (int i = 0; i < 20; i++)
          {
              aquarium.Add(new Fish($"aaa+{i}"));
          }

          Assert.Throws<InvalidOperationException>((() => aquarium.SellFish("aaa")), $"Fish with the name aaa doesn't exist!");

      }
      [Test]
      public void CheckSellFishMethodWhenFishExists()
      {
          for (int i = 0; i < 17; i++)
          {
              aquarium.Add(new Fish($"aaa+{i}"));
          }
          aquarium.Add(fish);
         Assert.AreEqual(fish, aquarium.SellFish(fish.Name));
      }
      [Test]
      public void CheckReportMethod()
      {
          Fish fish2 = new Fish("bbb");
          Fish fish3 = new Fish("ccc");
          aquarium.Add(fish);
          aquarium.Add(fish2);
          aquarium.Add(fish3);
          Assert.AreEqual($"Fish available at {aquarium.Name}: aaa, bbb, ccc", aquarium.Report());
        }

    }
}
