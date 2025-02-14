using ConsoleApp.Application.Services;
using ConsoleApp.Domain.Zoo.Entities;
using ConsoleApp.Domain.Zoo.Interfaces;
using ConsoleApp.Domain.Zoo.Services;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
  public static void Main(string[] args)
  {
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    var services = new ServiceCollection();
    services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
    services.AddSingleton<Zoo>();
    services.AddSingleton<ZooManagementService>();
    var serviceProvider = services.BuildServiceProvider();

    var zoo = serviceProvider.GetService<Zoo>();
    var managementService = serviceProvider.GetService<ZooManagementService>();

    while (true)
    {
      try
      {
        managementService.DisplayMenu();
        string choice = Console.ReadLine();
        Console.Clear();

        switch (choice)
        {
          case "1":
            managementService.AddAnimal();
            break;
          case "2":
            managementService.AddThing();
            break;
          case "3":
            zoo.SumFood();
            break;
          case "4":
            zoo.ContactZoo();
            break;
          case "5":
            zoo.InventoryManagement();
            break;
          case "6":
            Console.WriteLine("Спасибо за использование системы управления зоопарком!");
            return;
          default:
            Console.WriteLine("❌ Неверный выбор. Пожалуйста, выберите число от 1 до 6.");
            break;
        }

        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
        Console.Clear();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Произошла ошибка: {ex.Message}");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        Console.Clear();
      }
    }
  }
}