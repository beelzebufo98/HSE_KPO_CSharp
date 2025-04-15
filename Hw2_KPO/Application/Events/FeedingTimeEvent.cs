using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.Events
{
  public class FeedingTimeEvent : DomainEvent
  {
    public Guid AnimalId { get; }
    public FoodType FoodType { get; }

    public FeedingTimeEvent(Guid animalId, FoodType foodType)
    {
      AnimalId = animalId;
      FoodType = foodType;
    }
  }
}
