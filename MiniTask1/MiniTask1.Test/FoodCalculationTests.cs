using Xunit;
using Moq;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Animals.Entities;
using System.Linq;
using ConsoleApp.Domain.Animals.Abstract;

namespace ConsoleApp.Tests.Animals
{
  public class FoodCalculationTests
  {
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public FoodCalculationTests()
    {
      _mockClinic = new Mock<IVeterinaryClinic>();
      _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void SumFood_CalculatesCorrectly()
    {
      // Arrange
      var tiger = new Tiger { Name = "Тигр", Food = 10, Number = 1, IsHealthy = true };
      var rabbit = new Rabbit { Name = "Кролик", Food = 5, Number = 2, IsHealthy = true, Kindness = 7 };
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
      _zoo.AddAnimal(tiger);
      _zoo.AddAnimal(rabbit);

      // Act & Assert
      _zoo.SumFood();
      Assert.Equal(15, _zoo.Animals.Sum(x => x.Food));
    }

    [Fact]
    public void AnimalAdded_EventFiresCorrectly()
    {
      // Arrange
      int totalAnimalsReceived = 0;
      int totalFoodReceived = 0;
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);

      _zoo.AnimalAdded += (totalAnimals, totalFood) =>
      {
        totalAnimalsReceived = totalAnimals;
        totalFoodReceived = totalFood;
      };

      var animal = new Tiger
      {
        Name = "Тигр",
        Food = 10,
        Number = 1,
        IsHealthy = true
      };

      // Act
      _zoo.AddAnimal(animal);

      // Assert
      Assert.Equal(1, totalAnimalsReceived);
      Assert.Equal(10, totalFoodReceived);
    }
  }
}