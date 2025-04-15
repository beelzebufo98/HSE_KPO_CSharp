using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using System.Runtime.InteropServices;

namespace Hw2_KPO.Domain.Entities
{
  public class Enclosure
  {
    public Guid Id { get; init; } = Guid.Empty;
    public EnclosureType EncType { get; init; }
    public int Size { get; init; }
    public int CurrentSize { get; set; }
    public List<Guid> AnimalsIds { get; private set; } = new();

    public Enclosure(EnclosureType type, int size)
    {
      Id = Guid.NewGuid();
      EncType = type;
      Size = size;
      CurrentSize = 0;

    }
    public bool IsCompatibleWithAnimal(AnimalType animalType)
    {
      return (EncType, animalType) switch
      {
        (EnclosureType.Cage, AnimalType.Mammal) => true,
        (EnclosureType.Cage, AnimalType.Reptile) => true,
        (EnclosureType.Aquarium, AnimalType.Fish) => true,
        (EnclosureType.Aquarium, AnimalType.Reptile) => true,
        (EnclosureType.Aquarium, AnimalType.Amphibian) => true,
        (EnclosureType.Pen, AnimalType.Mammal) => true,
        (EnclosureType.Pen, AnimalType.Amphibian) => true,
        (EnclosureType.Aviary, AnimalType.Bird) => true,
        _ => false
      };
    }
    public bool AddAnimal(Guid animalId, AnimalType type)
    {
      if (CurrentSize < 0)
      {
        CurrentSize = 0;
      }

      if ((CurrentSize < Size) && (IsCompatibleWithAnimal(type)))
      {
        CurrentSize++;
        AnimalsIds.Add(animalId);
        return true;
      }
      return false;
      
    }

    public bool RemoveAnimal(Guid animalId)
    {
      if (CurrentSize > 0)
      {
        var elem = AnimalsIds.FirstOrDefault(a => a == animalId);
        if (elem != null) {
          AnimalsIds.Remove(elem);
          CurrentSize--;
          return true;
        }
      }
      return false;
    }
    public void Clean()
    {
      AnimalsIds.Clear();
      CurrentSize = 0;
    }
  }
}
