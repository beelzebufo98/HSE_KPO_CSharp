using Xunit;
using Moq;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Animals.Entities;
using ConsoleApp.Domain.Animals.Abstract;

namespace ConsoleApp.Tests.Animals
{
  public class ContactZooTests
  {
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public ContactZooTests()
    {
      _mockClinic = new Mock<IVeterinaryClinic>();
      _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void Contact_KindAnimals_Included()
    {
      // Arrange
      var kindRabbit = new Rabbit
      {
        Name = "Добрый кролик",
        Food = 5,
        Number = 1,
        IsHealthy = true,
        Kindness = 8
      };
      var unkindRabbit = new Rabbit
      {
        Name = "Злой кролик",
        Food = 5,
        Number = 2,
        IsHealthy = true,
        Kindness = 3
      };
      _mockClinic.Setup(c => c.CheckHealth(It.IsAny<Animal>())).Returns(true);
      _zoo.AddAnimal(kindRabbit);
      _zoo.AddAnimal(unkindRabbit);

      // Act
      _zoo.ContactZoo();

      // Assert
      Assert.Single(_zoo.ContactAnimals);
      Assert.Contains(kindRabbit, _zoo.ContactAnimals);
      Assert.DoesNotContain(unkindRabbit, _zoo.ContactAnimals);
    }
  }
}