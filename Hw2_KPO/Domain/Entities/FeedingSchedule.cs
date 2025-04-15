using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Domain.Entities
{
  public class FeedingSchedule
  {
    public Guid Id { get; init; }
    public AnimalType TypeAnimal { get; init; }
    public TimeSpan Time { get; set; }
      
    public FoodType FType { get; init; }

    public bool IsActive { get; set; } = true;

    public FeedingSchedule(AnimalType type, TimeSpan time, FoodType ftype)
    {
      Id = Guid.NewGuid();
      TypeAnimal = type;
      Time = time;
      FType = ftype;
    }

    public void UpdateFeedingSchedule(TimeSpan feedingTime)
    {
      TimeSpan newTime = feedingTime;
    }
    public void ActivateFeedingSchedule()
    {
      IsActive = true;
    }
    public void CancelFeedingSchedule()
    {
      IsActive = false;
    }
  }
}
