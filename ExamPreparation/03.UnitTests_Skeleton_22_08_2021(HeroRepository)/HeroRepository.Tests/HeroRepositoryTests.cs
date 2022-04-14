using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private HeroRepository heroRepository;
    private Hero hero;
    [SetUp]
    public void SetUp()
    {
        heroRepository = new HeroRepository();
        hero = new Hero("Pesho", 15);
    }

    [Test]

    public void CheckCollectionExists()
    {
        Assert.IsNotNull(heroRepository.Heroes);
    }

    [Test]

    public void Exception_CannotCreatHeroWhenHeIsNull()
    {
        hero = null;
        Assert.Throws<ArgumentNullException>(() => heroRepository.Create(hero), "Hero is null");
    }
    [Test]

    public void Exception_CannotCreatHeroWhenNameAlreadyExists()
    {
        Hero hero2 = new Hero("Gosho", 5);
        Hero hero3 = new Hero("Pesho", 15);
        heroRepository.Create(hero);
        heroRepository.Create(hero2);
        Assert.Throws<InvalidOperationException>(() => heroRepository.Create(hero3), $"Hero with name {hero3.Name} already exists");
    }
    [Test]

    public void CheckMethodCreateHero()
    {
        Assert.AreEqual($"Successfully added hero {hero.Name} with level {hero.Level}", heroRepository.Create(hero));
    }
    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void Exception_CannotRemoveHeroWithNameNullOrWhitespace(string heroName)
    {

        Hero testHero = new Hero(heroName, 5);
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(testHero.Name), "Name cannot be null");
    }
    [Test]
   public void CheckMethodRemoveHeroByName()
    {
        Hero hero2 = new Hero("Gosho", 5);
        Hero hero3 = new Hero("Maimun", 15);
        heroRepository.Create(hero);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);
      Assert.IsTrue(heroRepository.Remove(hero3.Name));
    }

   [Test]
   public void CheckMethodGetHeroWithHighestLevel()
   {
       Hero hero2 = new Hero("Gosho", 5);
       Hero hero3 = new Hero("Maimun", 17);
       heroRepository.Create(hero);
       heroRepository.Create(hero2);
       heroRepository.Create(hero3);
       Assert.AreEqual(hero3, heroRepository.GetHeroWithHighestLevel());

    }
   [Test]
   public void CheckMethodGetHeroByName()
   {
       Hero hero2 = new Hero("Gosho", 5);
       Hero hero3 = new Hero("Maimun", 17);
       heroRepository.Create(hero);
       heroRepository.Create(hero2);
       heroRepository.Create(hero3);
       Assert.AreEqual(hero3, heroRepository.GetHero("Maimun"));

   }

}