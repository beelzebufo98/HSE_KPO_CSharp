using ConsoleApp.Domain.Animals.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Animals.Abstract
{
  public abstract class Animal : IAlive, IInventory
  {
    public required int Food { get; set; }
    public required string Name { get; set; }
    public required bool IsHealthy { get; set; }
    public required int Number { get; set; }
  }
}
