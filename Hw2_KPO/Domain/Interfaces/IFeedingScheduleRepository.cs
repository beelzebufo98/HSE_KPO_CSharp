using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Domain.Interfaces
{
  public interface IFeedingScheduleRepository
  {
    public FeedingSchedule GetById(Guid id);

    public IEnumerable<FeedingSchedule> GetAll();

    public IEnumerable<FeedingSchedule> GetByAnimalType(AnimalType type);

    public void AddFeedingSchedule(FeedingSchedule feedingSchedule);
    public void RemoveFeedingSchedule(Guid id);


  }
}
