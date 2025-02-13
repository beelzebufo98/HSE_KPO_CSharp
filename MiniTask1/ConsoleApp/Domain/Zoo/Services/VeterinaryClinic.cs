using ConsoleApp.Domain.Animals.Abstract;
using ConsoleApp.Domain.Zoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Zoo.Services
{
  
  public class VeterinaryClinic : IVeterinaryClinic
  {
    public bool CheckHealth(Animal animal)
    {
      return animal.IsHealthy;
    }
  }
}
