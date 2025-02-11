using KPO_ConsoleApp.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO_ConsoleApp
{
  public class VeterinaryClinic
  {
    public bool CheckHealth(Animal animal)
    {
      return animal.IsHealthy;
    }
  }
}
