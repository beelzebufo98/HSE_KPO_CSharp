using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Hw2_KPO.Domain.Entities
{
  public class Animal
  {
    public Guid Id { get; init; }
    public AnimalType Type { get; init; }
    public string Name { get; init; }
    public DateTime Date { get; init; }
    public string Gender { get; init; }
    public FoodType FavorFood { get; init; }
    public bool isHealthy { get; private set; }  = true;
    public bool isHappily { get; private set; } = true;
    public Guid enclosureId { get; set; } = Guid.Empty;


    public Animal(AnimalType type, string name, DateTime date, string gender, FoodType favorFood)
    {
      Id = Guid.NewGuid();
      Type = type;
      Name = name;
      Date = date;
      Gender = gender;
      FavorFood = favorFood;
      this.isHealthy = true;
      this.isHappily = true;
      enclosureId = Guid.Empty;

    }
    public void Feed(FoodType type)
    {
      isHappily = true;
    }
    public void Heal()
    {
      if (!isHealthy)
      {
        isHealthy = true;
      }
    }  
    public void SetSick()
    {
      if (isHealthy)
      { 
        isHealthy = false;
        isHappily = false;
      }
    }
  }
}
