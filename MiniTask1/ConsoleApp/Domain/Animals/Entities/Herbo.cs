using ConsoleApp.Domain.Animals.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Animals.Entities
{
  public class Herbo : Animal
  {
    public required int Kindness { get; set; }
  }
}
