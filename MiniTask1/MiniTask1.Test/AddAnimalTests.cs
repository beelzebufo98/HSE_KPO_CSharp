using Xunit;
using Moq;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Animals.Abstract;
using ConsoleApp.Domain.Animals.Entities;

namespace ConsoleApp.Tests.Animals
{
  public class AddAnimalTests
  {
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public AddAnimalTests()
    {
      _mockClinic = new Mock<IVeterinaryClinic>();
      _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void Add_HealthyAnimal_Success()
    {
      // Arrange
      var animal = new Tiger
      {
        Name = "Тигр",
        Food = 10,
        Number = 1,
        IsHealthy = true
      };
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);

      // Act
      bool result = _zoo.AddAnimal(animal);

      // Assert
      Assert.True(result);
      Assert.Single(_zoo.Animals);
      Assert.Contains(animal, _zoo.Animals);
    }

    [Fact]
    public void Add_UnhealthyAnimal_Fails()
    {
      // Arrange
      var animal = new Tiger
      {
        Name = "Тигр",
        Food = 10,
        Number = 1,
        IsHealthy = false
      };
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(false);

      // Act
      bool result = _zoo.AddAnimal(animal);

      // Assert
      Assert.False(result);
      Assert.Empty(_zoo.Animals);
    }

    [Fact]
    public void Add_DuplicateNumber_Fails()
    {
      // Arrange
      var animal1 = new Tiger
      {
        Name = "Тигр1",
        Food = 10,
        Number = 1,
        IsHealthy = true
      };
      var animal2 = new Wolf
      {
        Name = "Волк",
        Food = 8,
        Number = 1,
        IsHealthy = true
      };
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
      _zoo.AddAnimal(animal1);

      // Act
      bool result = _zoo.AddAnimal(animal2);

      // Assert
      Assert.False(result);
      Assert.Single(_zoo.Animals);
    }
  }
}