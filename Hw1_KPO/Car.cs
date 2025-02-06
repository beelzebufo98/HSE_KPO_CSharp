using System;


namespace Hw1_KPO
{
  internal class Car
  {
    public int Number {  get; set; }
    public Engine engine { get; set; }
    private static int _count = 0;
    public Car() {
      this.Number = ++_count;
      engine = new Engine() { Size = 7 };
    }
    public override string ToString()
    {
      return $"Number: {Number}, Pedal size: {engine.Size}";
    }
  }
}
