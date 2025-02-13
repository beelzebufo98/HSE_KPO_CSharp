using Xunit;
using Moq;
using KPO_ConsoleApp;
using KPO_ConsoleApp.Animals;
using KPO_ConsoleApp.Things;
using System.Collections.Generic;

namespace KPO_ConsoleApp.Tests
{
    public class ZooTests
    {
        [Fact]
        public void AddAnimal_WhenHealthy_ShouldAddToList()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
            var zoo = new Zoo(mockClinic.Object);
            var animal = new Tiger
            {
                Name = "TestTiger",
                Food = 10,
                IsHealthy = true,
                Number = 1
            };

            // Act
            zoo.AddAnimal(animal);

            // Assert
            Assert.Single(zoo.Animals);
            Assert.Contains(animal, zoo.Animals);
        }
        [Fact]
        public void AddAnimal_WhenUnhealthy_ShouldNotAddToList()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(false);
            var zoo = new Zoo(mockClinic.Object);
            var animal = new Tiger
            {
                Name = "SickTiger",
                Food = 10,
                IsHealthy = false,
                Number = 1
            };

            // Act
            zoo.AddAnimal(animal);

            // Assert
            Assert.Empty(zoo.Animals);
        }

        [Fact]
        public void ContactZoo_ShouldReturnOnlyFriendlyHerbos()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
            var zoo = new Zoo(mockClinic.Object);
            var friendlyRabbit = new Rabbit { Name = "Friendly", Kindness = 8, Food = 2, IsHealthy = true, Number = 1 };
            var unfriendlyRabbit = new Rabbit { Name = "Unfriendly", Kindness = 3, Food = 2, IsHealthy = true, Number = 2 };
            var tiger = new Tiger { Name = "Tiger", Food = 10, IsHealthy = true, Number = 3 };

            zoo.AddAnimal(friendlyRabbit);
            zoo.AddAnimal(unfriendlyRabbit);
            zoo.AddAnimal(tiger);

            // Act
            zoo.ContactZoo();

            // Assert
            Assert.Single(zoo.ContactAnimals);
            Assert.Contains(friendlyRabbit, zoo.ContactAnimals);
            Assert.DoesNotContain(unfriendlyRabbit, zoo.ContactAnimals);
        }

        [Fact]
        public void SumFood_ShouldCalculateTotalFoodCorrectly()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
            var zoo = new Zoo(mockClinic.Object);
            var rabbit = new Rabbit { Name = "Rabbit", Food = 2, IsHealthy = true, Number = 1, Kindness = 5 };
            var tiger = new Tiger { Name = "Tiger", Food = 10, IsHealthy = true, Number = 2 };
            zoo.AddAnimal(rabbit);
            zoo.AddAnimal(tiger);

            // Act
            int totalFood = zoo.Animals.Sum(x => x.Food);

            // Assert
            Assert.Equal(12, totalFood);
        }
    }

    public class VeterinaryClinicTests
    {
        [Fact]
        public void CheckHealth_WhenHealthy_ReturnsTrue()
        {
            // Arrange
            IVeterinaryClinic clinic = new VeterinaryClinic();
            var animal = new Tiger { Name = "Tobi", Food = 10, IsHealthy = true, Number = 5 };

            // Act
            var result = clinic.CheckHealth(animal);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckHealth_WhenUnhealthy_ReturnsFalse()
        {
            // Arrange
            IVeterinaryClinic clinic = new VeterinaryClinic();
            var animal = new Tiger { Name = "Ben", Food = 10, IsHealthy = false, Number = 10 };

            // Act
            var result = clinic.CheckHealth(animal);

            // Assert
            Assert.False(result);
        }
    }

    public class ZooManagementServiceTests
    {
        //[Fact]
        //public void CreateAnimal_WithValidType_ReturnsCorrectAnimalType()
        //{
        //    // Arrange
        //    var mockClinic = new Mock<IVeterinaryClinic>();
        //    var zoo = new Zoo(mockClinic.Object);
        //    var service = new ZooManagementService(zoo);

        //    // Act
        //    var animal = service.CreateAnimal("1", "TestRabbit", 19, 15, true);

        //    // Assert
        //    Assert.IsType<Rabbit>(animal);
        //    Assert.Equal("TestRabbit", animal.Name);
        //    Assert.Equal(5, animal.Food);
        //    Assert.Equal(12, animal.Number);
        //    Assert.True(animal.IsHealthy);
        //}

        [Fact]
        public void CreateThing_WithValidType_ReturnsCorrectThingType()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            var zoo = new Zoo(mockClinic.Object);
            var service = new ZooManagementService(zoo);

            // Act
            var thing = service.CreateThing("1", "TestTable", 1);

            // Assert
            Assert.IsType<Table>(thing);
            Assert.Equal("TestTable", thing.Name);
            Assert.Equal(1, thing.Number);
        }

        [Fact]
        public void IsInventoryNumberUnique_WithUniqueNumber_ReturnsTrue()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            var zoo = new Zoo(mockClinic.Object);
            var existingAnimal = new Tiger { Number = 1, Name = "Tiger", Food = 10, IsHealthy = true };
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
            zoo.AddAnimal(existingAnimal);

            // Act
            var result = zoo.IsInventoryNumberUnique(2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInventoryNumberUnique_WithDuplicateNumber_ReturnsFalse()
        {
            // Arrange
            var mockClinic = new Mock<IVeterinaryClinic>();
            var zoo = new Zoo(mockClinic.Object);
            var existingAnimal = new Tiger { Number = 1, Name = "Tiger", Food = 10, IsHealthy = true };
            mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
            zoo.AddAnimal(existingAnimal);

            // Act
            var result = zoo.IsInventoryNumberUnique(1);

            // Assert
            Assert.False(result);
        }
    }
}