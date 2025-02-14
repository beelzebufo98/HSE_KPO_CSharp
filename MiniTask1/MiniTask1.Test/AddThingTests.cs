using Xunit;
using Moq;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Things.Entities;

namespace ConsoleApp.Tests.Things
{
  public class AddThingTests
  {
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public AddThingTests()
    {
      _mockClinic = new Mock<IVeterinaryClinic>();
      _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void Add_UniqueThing_Success()
    {
      // Arrange
      var thing = new Table { Name = "Стол", Number = 1 };

      // Act
      bool result = _zoo.AddThing(thing);

      // Assert
      Assert.True(result);
      Assert.Single(_zoo.Things);
      Assert.Contains(thing, _zoo.Things);
    }

    [Fact]
    public void Add_DuplicateNumber_Fails()
    {
      // Arrange
      var thing1 = new Table { Name = "Стол", Number = 1 };
      var thing2 = new Computer { Name = "Компьютер", Number = 1 };
      _zoo.AddThing(thing1);

      // Act
      bool result = _zoo.AddThing(thing2);

      // Assert
      Assert.False(result);
      Assert.Single(_zoo.Things);
    }
  }
}