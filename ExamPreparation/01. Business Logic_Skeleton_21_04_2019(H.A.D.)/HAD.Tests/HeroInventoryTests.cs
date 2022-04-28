using HAD.Contracts;
using HAD.Entities.Items;
using HAD.Entities.Miscellaneous;
using NUnit.Framework;


namespace HAD.Tests
{

    public class HeroInventoryTests
    {
        private HeroInventory heroInventory;
        private CommonItem commonItem1;
        private CommonItem commonItem2;
        private IRecipe recipeitem1;
        private IRecipe recipeitem2;
        [SetUp]
        public void Setup()
        {
            heroInventory = new HeroInventory();
            commonItem1 = new CommonItem("aaa", 5, 5, 10, 10, 10);
            commonItem2 = new CommonItem("bbb", 10, 10, 5, 5, 5);
            recipeitem1 = new RecipeItem("aaa11", 15, 15, 30, 30, 30, "aaa");
            recipeitem2 = new RecipeItem("bbb11", 7, 9, 9, 35, 35, "bbb");

        }
        [Test]
        public void CheckAddNewItemsToCommonItems()
        {
            heroInventory.AddCommonItem(commonItem1);
            heroInventory.AddCommonItem(commonItem2);

            Assert.AreEqual(2, heroInventory.CommonItems.Count);
        }
        [Test]
        public void CheckAddNewRecipeItemsAndCombineRecipeMethods()
        {
            heroInventory.AddCommonItem(commonItem1);
            heroInventory.AddCommonItem(commonItem2);
            heroInventory.AddRecipeItem(recipeitem1);
            heroInventory.AddRecipeItem(recipeitem2);
            Assert.AreEqual(2, heroInventory.CommonItems.Count);
            Assert.AreEqual(22, heroInventory.TotalStrengthBonus);
            Assert.AreEqual(24, heroInventory.TotalAgilityBonus);
            Assert.AreEqual(39, heroInventory.TotalIntelligenceBonus);
            Assert.AreEqual(65, heroInventory.TotalHitPointsBonus);
            Assert.AreEqual(65, heroInventory.TotalDamageBonus);

        }
    }
}