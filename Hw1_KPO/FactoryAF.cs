using System;


namespace Hw1_KPO
{
  internal class FactoryAF
  {
    public List<Car> cars;
    public List<Customer> customers;
   
    public FactoryAF(List<Customer> customers)
    {
      this.customers = customers;
      this.cars = new List<Car>();
    }
    internal void SaleCar()
    {
      foreach (var customer in customers)
      {
        if (customer.car == null && cars.Count !=0)
        {
          customer.car = cars.Last();
          cars.RemoveAt(cars.Count - 1);
        }
      }
      customers = customers.Where(customer => customer.car != null).ToList();
      cars.Clear();
    }
    internal void AddCar()
    {
      Car car = new Car();
      cars.Add(car);
    }
  }
}
