using KPO_ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO_ConsoleApp.Things
{
  public class Thing: IInventory
  {
    public int Number {  get; set; }
    public string Name { get; set; }
  }
}
