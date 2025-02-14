using Xunit;
using ConsoleApp.Domain.Animals.Entities;
using ConsoleApp.Domain.Zoo.Services;

namespace ConsoleApp.Tests.Services
{
  public class VeterinaryClinicTests
  {
    [Fact]
    public void CheckHealth_ReturnsExpectedResult()
    {
      // Arrange
      var clinic = new VeterinaryClinic();
      var healthyAnimal = new Tiger { Name = "Тигр", Food = 10, Number = 1, IsHealthy = true };
      var unhealthyAnimal = new Tiger { Name = "Тигр", Food = 10, Number = 2, IsHealthy = false };

      // Act
      var healthyResult = clinic.CheckHealth(healthyAnimal);
      var unhealthyResult = clinic.CheckHealth(unhealthyAnimal);

      // Assert
      Assert.True(healthyResult);
      Assert.False(unhealthyResult);
    }
  }
}