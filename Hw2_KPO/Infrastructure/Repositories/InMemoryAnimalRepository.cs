using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Infrastructure.Data;

namespace Hw2_KPO.Infrastructure.Repositories
{
  public class InMemoryAnimalRepository : IAnimalRepository
  {
    private readonly InMemoryZooDatabase _database;
    private readonly ILogger<InMemoryAnimalRepository> _logger;

    public InMemoryAnimalRepository(InMemoryZooDatabase database, ILogger<InMemoryAnimalRepository> logger)
    {
      _database = database;
      _logger = logger;
    }

    public void AddAnimal(Animal animal)
    {
      _database.Animals.Add(animal);
      _logger.LogInformation("Added animal: {Name} ({Id})", animal.Name, animal.Id);
    }
    public void Update(Animal animal)
    {
      var result = animal.Id;
      RemoveAnimal(result);
      _logger.LogInformation($"Update info for animal with Id = {result}");
      AddAnimal(animal);
    }
    public IEnumerable<Animal> GetAll()
    {
      return _database.Animals;
    }

    public Animal GetById(Guid id)
    {
      var result = _database.Animals.FirstOrDefault(a => a.Id == id);
      return result;
    }

    public void RemoveAnimal(Guid id)
    {
      var result = _database.Animals.FirstOrDefault(a => a.Id == id);
      if (result != null)
      {
        _database.Animals.Remove(result);
        _logger.LogInformation("Deleted animal: {Id}", id);
      }
    }
  }
}
