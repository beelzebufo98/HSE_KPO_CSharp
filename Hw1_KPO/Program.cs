
using Hw1_KPO;
internal class Program
{
  static void Main(string[] args)
  {
    var customers = new List<Customer>
            {
                new() { name = "Yamal", car = new Car()},
                new() { name = "Pedri" },
                new() { name = "Gavi" },
                new() { name = "Balde" }
            };

    var factory = new FactoryAF(customers);

    for (int i = 0; i < 2; i++)
      factory.AddCar();

    Console.WriteLine("Before");
    Console.WriteLine(string.Join(Environment.NewLine, factory.cars));
    Console.WriteLine(string.Join(Environment.NewLine, factory.customers));

    factory.SaleCar();

    Console.WriteLine("After");
    Console.WriteLine(string.Join(Environment.NewLine, factory.cars));
    Console.WriteLine(string.Join(Environment.NewLine, factory.customers));
  }
}