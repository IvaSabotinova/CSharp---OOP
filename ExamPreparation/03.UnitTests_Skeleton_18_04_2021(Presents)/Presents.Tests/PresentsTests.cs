using System;

namespace Presents.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {

        private Present present;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            present = new Present("aaa", 2);
            bag = new Bag();
        }

        [Test]

        public void CheckCreationOfPresent()
        {
            Assert.AreEqual("aaa", present.Name);
            Assert.AreEqual(2, present.Magic);

        }
        [Test]
        public void CheckBagAndPresentCtorWorkingProperly()
        {
            Assert.IsNotNull(present);
            Assert.IsNotNull(bag);
        }

        [Test]
        public void Exception_CheckAddMethodWhenPresentIsNull()
        {
            present = null;
            Assert.Throws<ArgumentNullException>((() => bag.Create(present)), "Present is null");
        }
        [Test]
        public void Exception_CheckAddMethodWhenPresentAlreadyExistsInBag()
        {
            Present present2 = new Present("bbb", 5);
            Present present3 = new Present("ccc", 7);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);
            Assert.Throws<InvalidOperationException>((() => bag.Create(present)), "This present already exists!");
        }
        [Test]
        public void CheckAddMethodAddingSuccessfullyNewPresent()
        {
            Assert.AreEqual($"Successfully added present {present.Name}.", bag.Create(present));
        }
        [Test]
        public void CheckRemoveMethod()
        {
            Present present2 = new Present("bbb", 5);
            Present present3 = new Present("ccc", 7);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            Assert.IsTrue(bag.Remove(present));
        }
        [Test]
        public void CheckMethodGetPresentWithLeastMagic()
        {
            Present present2 = new Present("bbb", 5);
            Present present3 = new Present("ccc", 7);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            Assert.AreEqual(present, bag.GetPresentWithLeastMagic());
        }
        [Test]
        public void CheckMethodGetPresentByName()
        {
            Present present2 = new Present("bbb", 5);
            Present present3 = new Present("ccc", 7);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            Assert.AreEqual(present, bag.GetPresent("aaa"));
        }

    }
}
