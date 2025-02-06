using System;
using System.Xml.Linq;


namespace Hw1_KPO
{
  internal class Customer
  {
    public Car? car {  get; set; } 
    public string name { get; set; }
    public override string ToString()
    {
      return $"Name: {name}, Car number: {car?.Number ?? -1}";
    }
  }
}
