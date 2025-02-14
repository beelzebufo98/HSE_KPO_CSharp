using ConsoleApp.Domain.Animals.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Things.Abstract
{
  public abstract class Thing : IInventory
  {
    public int Number { get; set; }
    public string Name { get; set; }
  }
}
