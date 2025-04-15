using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using Hw2_KPO.Infrastructure.Data;

namespace Hw2_KPO.Infrastructure.Repositories
{
  public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
  {
    private readonly InMemoryZooDatabase _database;
    private readonly ILogger<InMemoryFeedingScheduleRepository> _logger;

    public InMemoryFeedingScheduleRepository(InMemoryZooDatabase database, ILogger<InMemoryFeedingScheduleRepository> logger)
    {
      _database = database;
      _logger = logger;
    }

    public void AddFeedingSchedule(FeedingSchedule feedingSchedule)
    {
      _database.FeedingSchedules.Add(feedingSchedule);
      _logger.LogInformation("Added feeding schedule: {Id}", feedingSchedule.Id);
    }

    public IEnumerable<FeedingSchedule> GetAll()
    {
      return _database.FeedingSchedules;
    }

    public IEnumerable<FeedingSchedule> GetByAnimalType(AnimalType type)
    {
      var result = _database.FeedingSchedules.Where(x => x.TypeAnimal == type).ToList();
      return result;
    }

    public FeedingSchedule GetById(Guid id)
    {
      var result = _database.FeedingSchedules.FirstOrDefault(x => x.Id == id);
      return result;
    }

    public void RemoveFeedingSchedule(Guid id)
    {
      var result = _database.FeedingSchedules.FirstOrDefault(x => x.Id == id);
      if (result != null)
      {
        _database.FeedingSchedules.Remove(result);
      }
    }
  }
}
