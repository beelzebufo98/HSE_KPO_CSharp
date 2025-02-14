using ConsoleApp.Domain.Animals.Abstract;
using ConsoleApp.Domain.Animals.Entities;
using ConsoleApp.Domain.Things.Abstract;
using ConsoleApp.Domain.Things.Entities;
using ConsoleApp.Domain.Zoo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Application.Services
{
  public class ZooManagementService
  {
    private readonly Zoo _zoo;

    public ZooManagementService(Zoo zoo)
    {
      _zoo = zoo;
    }

    public void DisplayMenu()
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

    public void AddAnimal()
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

          if (_zoo.IsInventoryNumberUnique(number))
            break;

          Console.WriteLine($"❌ Инвентарный номер {number} уже используется. Введите другой номер.");
        }

        bool isHealthy = GetValidInput("Животное здорово? (да/нет): ",
            input => input.ToLower() == "да" || input.ToLower() == "нет") == "да";

        Animal animal = CreateAnimal(typeChoice, name, food, number, isHealthy);

        if (_zoo.AddAnimal(animal))
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

    public Animal CreateAnimal(string typeChoice, string name, int food, int number, bool isHealthy)
    {
      return typeChoice switch
      {
        "1" => new Rabbit
        {
          Name = name,
          Food = food,
          Number = number,
          IsHealthy = isHealthy,
          Kindness = GetValidIntInput("Введите уровень доброты (1-10): ",
                value => value >= 1 && value <= 10)
        },
        "2" => new Tiger
        {
          Name = name,
          Food = food,
          Number = number,
          IsHealthy = isHealthy
        },
        "3" => new Monkey
        {
          Name = name,
          Food = food,
          Number = number,
          IsHealthy = isHealthy,
          Kindness = GetValidIntInput("Введите уровень доброты (1-10): ",
                value => value >= 1 && value <= 10)
        },
        "4" => new Wolf
        {
          Name = name,
          Food = food,
          Number = number,
          IsHealthy = isHealthy
        },
        _ => throw new ArgumentException("Неверный тип животного")
      };
    }

    public void AddThing()
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

          if (_zoo.IsInventoryNumberUnique(number))
            break;

          Console.WriteLine($"❌ Инвентарный номер {number} уже используется. Введите другой номер.");
        }

        Thing thing = CreateThing(typeChoice, name, number);

        if (_zoo.AddThing(thing))
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

    public Thing CreateThing(string typeChoice, string name, int number)
    {
      return typeChoice switch
      {
        "1" => new Table { Name = name, Number = number },
        "2" => new Computer { Name = name, Number = number },
        _ => throw new ArgumentException("Неверный тип предмета")
      };
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
}
