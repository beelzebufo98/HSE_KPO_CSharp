using ConsoleApp.Domain.Animals.Abstract;
using ConsoleApp.Domain.Animals.Entities;
using ConsoleApp.Domain.Things.Abstract;
using ConsoleApp.Domain.Zoo.Interfaces;

namespace ConsoleApp.Domain.Zoo.Entities
{

  public class Zoo
  {
    public List<Animal> Animals { get; private set; } = new List<Animal>();
    public List<Herbo> ContactAnimals { get; private set; }
    public List<Thing> Things { get; private set; } = new List<Thing>();

    private readonly IVeterinaryClinic Clinic;

    public event Action<int, int> AnimalAdded;


    public Zoo(IVeterinaryClinic clinic)
    {
      this.Clinic = clinic;
      AnimalAdded += OnAnimalAdded;
    }
    public bool IsInventoryNumberUnique(int number)
    {
      if (Animals.Any(a => a.Number == number))
        return false;

      if (Things.Any(t => t.Number == number))
        return false;

      return true;
    }
    private void OnAnimalAdded(int totalAnimals, int totalFood)
    {
      Console.WriteLine($"Теперь в зоопарке {totalAnimals} животных.");
      Console.WriteLine($"Общее количество потребляемой еды в день: {totalFood}.");
    }
    public void CountAnimals()
    {
      Console.WriteLine($"Общее количество животных в зоопарке составляет {Animals.Count()}");
    }

    public void SumFood()
    {
      int sum = Animals.Sum(x => x.Food);
      Console.WriteLine($"Общее количество потребляемой еды в день всеми животными составляет {sum}");
    }
    public void ContactZoo()
    {
      ContactAnimals = Animals.OfType<Herbo>().Where(x => x.Kindness > 5).ToList();
      Console.WriteLine("Животные для контактного зоопарка");
      int nameWidth = 20;
      int numberWidth = 15;
      Console.WriteLine($"{"Name".PadRight(nameWidth)}{"Kindness level".PadRight(numberWidth)}");
      Console.WriteLine(new string('-', nameWidth + numberWidth));
      foreach (Herbo animal in ContactAnimals)
      {
        Console.WriteLine($"{animal.Name.PadRight(nameWidth)}{animal.Kindness.ToString().PadRight(numberWidth)}");
      }
    }

    public bool AddAnimal(Animal animal)
    {
      if (!IsInventoryNumberUnique(animal.Number))
      {
        Console.WriteLine($"❌ Ошибка: Инвентарный номер {animal.Number} уже используется.");
        return false;
      }

      if (Clinic.CheckHealth(animal))
      {
        Animals.Add(animal);
        Console.WriteLine($"✅ {animal.Name} был(-а) добавлен(-а) в зоопарк");
        AnimalAdded?.Invoke(Animals.Count, Animals.Sum(x => x.Food));
        return true;
      }
      else
      {
        Console.WriteLine($"❌ {animal.Name} отказано в добавлении в зоопарк по состоянию здоровья");
        return false;
      }
    }
    public bool AddThing(Thing thing)
    {
      if (!IsInventoryNumberUnique(thing.Number))
      {
        Console.WriteLine($"❌ Ошибка: Инвентарный номер {thing.Number} уже используется.");
        return false;
      }

      Things.Add(thing);
      Console.WriteLine($"✅ {thing.Name} успешно добавлен в инвентарь");
      return true;
    }
    public void InventoryManagement()
    {
      Console.WriteLine("---ИНВЕНТАРИЗАЦИЯ---");
      Console.WriteLine($"=== ANIMALS ===");
      int nameWidth = 20;
      int numberWidth = 15;
      Console.WriteLine($"{"Name".PadRight(nameWidth)}{"Number".PadRight(numberWidth)}");
      Console.WriteLine(new string('-', nameWidth + numberWidth));
      foreach (Animal animal in Animals)
      {
        Console.WriteLine($"{animal.Name.PadRight(nameWidth)}{animal.Number.ToString().PadRight(numberWidth)}");
      }
      Console.WriteLine("=== THINGS ===");
      Console.WriteLine($"{"Name".PadRight(nameWidth)}{"Number".PadRight(numberWidth)}");
      Console.WriteLine(new string('-', nameWidth + numberWidth));
      foreach (Thing thing in Things)
      {
        Console.WriteLine($"{thing.Name.PadRight(nameWidth)}{thing.Number.ToString().PadRight(numberWidth)}");
      }
    }
  }


}