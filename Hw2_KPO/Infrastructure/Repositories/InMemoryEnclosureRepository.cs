using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Infrastructure.Data;

namespace Hw2_KPO.Infrastructure.Repositories
{
  public class InMemoryEnclosureRepository:IEnclosureRepository
  {
    private readonly InMemoryZooDatabase _database;
    private readonly ILogger<InMemoryEnclosureRepository> _logger;

    public InMemoryEnclosureRepository(InMemoryZooDatabase database, ILogger<InMemoryEnclosureRepository> logger)
    {
      _database = database;
      _logger = logger;
    }
    public IEnumerable<Animal> GetAnimalsInEnclosure(Guid id)
    {
      var result = _database.Enclosures.FirstOrDefault(enclosure => enclosure.Id == id);
      foreach(var animal in result.AnimalsIds)
      {
        var animalResult = _database.Animals.FirstOrDefault(a => a.Id == animal);
        if (animalResult != null)
        {
          yield return animalResult;
        }
      }
    }
    public void AddEnclosure(Enclosure enclosure)
    {
      _database.Enclosures.Add(enclosure);
    }

    public void Update(Enclosure enclosure)
    {
      var result = enclosure.Id;
      RemoveEnclosure(result);
      _logger.LogInformation($"Update info for enclosure with Id = {result}");
      AddEnclosure(enclosure);
    }
    public IEnumerable<Enclosure> GetAll()
    {
      return _database.Enclosures;
    }

    public Enclosure GetById(Guid id)
    {
      var result = _database.Enclosures.FirstOrDefault(enclosure => enclosure.Id == id);
      return result;
    }

    public void RemoveEnclosure(Guid id)
    {
      var result = _database.Enclosures.FirstOrDefault(enclosure => enclosure.Id == id);
      if (result != null)
      {
        _database.Enclosures.Remove(result);
        _logger.LogInformation("Deleted enclosure: {Id}", id);
      }

    }
  }
}
