using Xunit;
using Moq;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Animals.Entities;
using ConsoleApp.Domain.Things.Entities;
using ConsoleApp.Domain.Animals.Abstract;

namespace ConsoleApp.Tests.Inventory
{
  public class InventoryNumberTests
  {
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public InventoryNumberTests()
    {
      _mockClinic = new Mock<IVeterinaryClinic>();
      _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void IsNumberUnique_ExistingAnimal_Fails()
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
      _zoo.AddAnimal(animal);

      // Act
      bool result = _zoo.IsInventoryNumberUnique(1);

      // Assert
      Assert.False(result);
    }

    [Fact]
    public void IsNumberUnique_ExistingThing_Fails()
    {
      // Arrange
      var thing = new Table { Name = "Стол", Number = 1 };
      _zoo.AddThing(thing);

      // Act
      bool result = _zoo.IsInventoryNumberUnique(1);

      // Assert
      Assert.False(result);
    }
  }
}