using NUnit.Framework;
using System;


namespace BankSafe.Tests
{
    [TestFixture]
    public class BankVaultTests
    {
        private BankVault bankVault;

        private Item item;

        //private Dictionary<string, Item> bankCells;
        [SetUp]
        public void Setup()
        {
            bankVault = new BankVault();
            item = new Item("aaa", "111");
       
           
        }

        [Test]

        public void TestCtor_Creating_Creating_VAlid_Item()
        {
            item = new Item("Gosho", "55555");
            Assert.IsNotNull(item);
        }

        [Test]
        public void TestCtor_BankVault()
        {
            Assert.IsNotNull(bankVault.VaultCells);
        }

        [Test]
        public void Exception_AddItemWhenCellDoesntExist()
        {
            Assert.Throws<ArgumentException>(() => bankVault.AddItem("aa", item), "Cell doesn't exists!");
        }
        [Test]
        public void Exception_AddItemWhenCellIsAlreadyTaken()
        {
            bankVault.AddItem("A1", item);
            Assert.Throws<ArgumentException>(() => bankVault.AddItem("A1", item), "Cell is already taken!");
        }
        [Test]
        public void Exception_ItemIsAlreadyInCell()
        {
            bankVault.AddItem("A1", item);
            Assert.Throws<InvalidOperationException>(() => bankVault.AddItem("B1", item), "Item is already in cell!");
        }
        [Test]
        public void AddItemInCells()
        {
            //Item item1 = new Item("bbb", "222");
            //Item item2 = new Item("ccc", "333");
            //bankVault.AddItem("A1", item);
            //bankVault.AddItem("B1", item1);
            //bankVault.AddItem("C1", item2);
            //Assert.IsTrue(bankVault.VaultCells["A1"] != null);
           Assert.AreEqual($"Item:{item.ItemId} saved successfully!", bankVault.AddItem("A1", item));
        }

        [Test]
        public void Exception_RemoveItemFromCellThatDoesntExist()
        {
            Assert.Throws<ArgumentException>((() => bankVault.RemoveItem("c8", item)), "Cell doesn't exists!");
        }
        [Test]
        public void Exception_RemoveItemWhenItemInThatCellDoesntExist()
        {
            Item item1 = new Item("bbb", "222");
            Item item2 = new Item("ccc", "333");
            bankVault.AddItem("A1", item);
            bankVault.AddItem("B1", item1);
            bankVault.AddItem("C1", item2);
            Assert.Throws<ArgumentException>((() => bankVault.RemoveItem("A1", item1)), "Item in that cell doesn't exists!");
        }
        [Test]
        public void RemoveItemFromCell()
        {
            Item item1 = new Item("bbb", "222");
            Item item2 = new Item("ccc", "333");
            bankVault.AddItem("A1", item);
            bankVault.AddItem("B1", item1);
            bankVault.AddItem("C1", item2);
           Assert.AreEqual($"Remove item:{item.ItemId} successfully!", bankVault.RemoveItem("A1", item));
        }

    }
}