namespace CosmosX.Tests
{
    using CosmosX.Entities.Containers;
    using CosmosX.Entities.Modules.Absorbing;
    using CosmosX.Entities.Modules.Energy;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ModuleContainerTests
    {
        ModuleContainer moduleContainer;
        CryogenRod energyModule1;
        CryogenRod energyModule2;
        CooldownSystem cooldownSystem1;
        CooldownSystem cooldownSystem2;
        HeatProcessor heatProcessor;

        [SetUp]
        public void SetUp()
        {
            moduleContainer = new ModuleContainer(5);
            energyModule1 = new CryogenRod(1, 25);
            energyModule2 = new CryogenRod(2, 30);
            cooldownSystem1 = new CooldownSystem(3, 25);
            cooldownSystem2 = new CooldownSystem(4, 25);
            heatProcessor = new HeatProcessor(5, 50);

        }
        [Test]
        public void Exception_CheckMethodAddEnergyModule()
        {
            energyModule1 = null;
            Assert.Throws<ArgumentException>(() => moduleContainer.AddEnergyModule(energyModule1));
        }
        [Test]
        public void CheckMethodAddEnergyModule()
        {
            moduleContainer.AddEnergyModule(energyModule1);
            moduleContainer.AddEnergyModule(energyModule2);
            moduleContainer.AddAbsorbingModule(cooldownSystem1);
            moduleContainer.AddAbsorbingModule(cooldownSystem2);
            moduleContainer.AddAbsorbingModule(heatProcessor);
            Assert.AreEqual(5, moduleContainer.ModulesByInput.Count);
            Assert.AreEqual(55, moduleContainer.TotalEnergyOutput);
            Assert.AreEqual(100, moduleContainer.TotalHeatAbsorbing);

        }
        [Test]
        public void CheckMethodAddEnergyModuleWhenModulesCountEqualsCapacity()
        {
            moduleContainer.AddEnergyModule(energyModule1);
            moduleContainer.AddEnergyModule(energyModule2);
            moduleContainer.AddAbsorbingModule(cooldownSystem1);
            moduleContainer.AddAbsorbingModule(cooldownSystem2);
            moduleContainer.AddAbsorbingModule(heatProcessor);
            moduleContainer.AddEnergyModule(new CryogenRod(6, 12));
            moduleContainer.AddAbsorbingModule(new HeatProcessor(7, 13));
            moduleContainer.AddAbsorbingModule(new CooldownSystem(8, 21));
            Assert.AreEqual(5, moduleContainer.ModulesByInput.Count);

        }
        [Test]
        public void Exception_CheckMethodAddAbsorbingModule()
        {
            cooldownSystem1 = null;
            Assert.Throws<ArgumentException>(() => moduleContainer.AddAbsorbingModule(cooldownSystem1));
        }

    }
}