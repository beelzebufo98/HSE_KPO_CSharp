using ConsoleApp.Domain.Animals.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Zoo.Interfaces
{
  public interface IVeterinaryClinic
  {
    bool CheckHealth(Animal animal);
  }
}
