using System;
using KPO_ConsoleApp;
using KPO_ConsoleApp.Animals;
using KPO_ConsoleApp.Things;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
  public static void Main(string[] args)
  {
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    var services = new ServiceCollection();
    services.AddSingleton<VeterinaryClinic>();
    services.AddSingleton<Zoo>();
    var serviceProvider = services.BuildServiceProvider();
    var zoo = serviceProvider.GetService<Zoo>();

    while (true)
    {
      try
      {
        DisplayMenu();
        string choice = Console.ReadLine();
        Console.Clear();

        switch (choice)
        {
          case "1":
            AddAnimal(zoo);
            break;
          case "2":
            AddThing(zoo);
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

  private static void DisplayMenu()
  {
    Console.WriteLine("=== Система управления зоопарком ===");
    Console.WriteLine("1. 🦁 Добавить животное");
    Console.WriteLine("2. 💻 Добавить инвентарь");
    Console.WriteLine("3. 🍖 Вывести отчет по питанию");
    Console.WriteLine("4. 🤝 Вывести список животных для контактного зоопарка");
    Console.WriteLine("5. 📋 Провести инвентаризацию");
    Console.WriteLine("6. ❌ Выйти");
    Console.Write("\nВыберите действие (1-6): ");
  }

  private static void AddAnimal(Zoo zoo)
  {
    bool addingAnimal = true;
    while (addingAnimal)
    {
      Console.WriteLine("=== Добавление нового животного ===\n");
      Console.WriteLine("Выберите тип животного:");
      Console.WriteLine("1. 🐰 Кролик");
      Console.WriteLine("2. 🐯 Тигр");
      Console.WriteLine("3. 🐒 Обезьяна");
      Console.WriteLine("4. 🐺 Волк");

      string typeChoice = GetValidInput("Выберите тип (1-4): ", input =>
          int.TryParse(input, out int result) && result >= 1 && result <= 4);

      string name = GetValidInput("Введите имя животного: ",
          input => !string.IsNullOrWhiteSpace(input));

      int food = GetValidIntInput("Введите количество еды (кг/день): ",
          value => value is > 0 and <= 100);

      int number;
      while (true)
      {
        number = GetValidIntInput("Введите инвентаризационный номер: ",
            value => value > 0);

        if (zoo.IsInventoryNumberUnique(number))
          break;

        Console.WriteLine($"❌ Инвентарный номер {number} уже используется. Введите другой номер.");
      }

      bool isHealthy = GetValidInput("Животное здорово? (да/нет): ",
          input => input.ToLower() == "да" || input.ToLower() == "нет") == "да";

      Animal animal = null;
      switch (typeChoice)
      {
        case "1":
          int kindness = GetValidIntInput("Введите уровень доброты (1-10): ",
              value => value >= 1 && value <= 10);
          animal = new Rabbit
          {
            Name = name,
            Food = food,
            Number = number,
            IsHealthy = isHealthy,
            Kindness = kindness
          };
          break;
        case "2":
          animal = new Tiger
          {
            Name = name,
            Food = food,
            Number = number,
            IsHealthy = isHealthy
          };
          break;
        case "3":
          int monkeyKindness = GetValidIntInput("Введите уровень доброты (1-10): ",
              value => value >= 1 && value <= 10);
          animal = new Monkey
          {
            Name = name,
            Food = food,
            Number = number,
            IsHealthy = isHealthy,
            Kindness = monkeyKindness
          };
          break;
        case "4":
          animal = new Wolf
          {
            Name = name,
            Food = food,
            Number = number,
            IsHealthy = isHealthy
          };
          break;
      }

      if (zoo.AddAnimal(animal))
      {
        addingAnimal = false;
      }
      else
      {
        if (GetValidInput("\nХотите попробовать добавить животное снова? (да/нет): ",
            input => input.ToLower() == "да" || input.ToLower() == "нет") != "да")
        {
          addingAnimal = false;
        }
        Console.Clear();
      }
    }
  }

  private static void AddThing(Zoo zoo)
  {
    bool addingThing = true;
    while (addingThing)
    {
      Console.WriteLine("=== Добавление нового инвентаря ===\n");
      Console.WriteLine("Выберите тип:");
      Console.WriteLine("1. 🪑 Стол");
      Console.WriteLine("2. 💻 Компьютер");

      string typeChoice = GetValidInput("Выберите тип (1-2): ",
          input => int.TryParse(input, out int result) && result >= 1 && result <= 2);

      string name = GetValidInput("Введите название: ",
          input => !string.IsNullOrWhiteSpace(input));

      int number;
      while (true)
      {
        number = GetValidIntInput("Введите инвентаризационный номер: ",
            value => value > 0);

        if (zoo.IsInventoryNumberUnique(number))
          break;

        Console.WriteLine($"❌ Инвентарный номер {number} уже используется. Введите другой номер.");
      }

      Thing thing = null;
      switch (typeChoice)
      {
        case "1":
          thing = new Table { Name = name, Number = number };
          break;
        case "2":
          thing = new Computer { Name = name, Number = number };
          break;
      }

      if (zoo.AddThing(thing))
      {
        addingThing = false;
      }
      else
      {
        if (GetValidInput("\nХотите попробовать добавить предмет снова? (да/нет): ",
            input => input.ToLower() == "да" || input.ToLower() == "нет") != "да")
        {
          addingThing = false;
        }
        Console.Clear();
      }
    }
  }
  private static string GetValidInput(string prompt, Func<string, bool> validator)
  {
    string input;
    do
    {
      Console.Write(prompt);
      input = Console.ReadLine();
      if (!validator(input))
      {
        Console.WriteLine("❌ Некорректный ввод. Попробуйте снова.");
      }
    } while (!validator(input));
    return input;
  }

  private static int GetValidIntInput(string prompt, Func<int, bool> validator)
  {
    while (true)
    {
      Console.Write(prompt);
      if (int.TryParse(Console.ReadLine(), out int result) && validator(result))
      {
        return result;
      }
      Console.WriteLine("❌ Некорректный ввод. Попробуйте снова.");
    }
  }
}