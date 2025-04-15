using Hw2_KPO.Application.Events;

namespace Hw2_KPO.Infrastructure.Services
{
  public class EventHandlerService
  {
    private readonly ILogger<EventHandlerService> _logger;

    public EventHandlerService(ILogger<EventHandlerService> logger)
    {
      _logger = logger;
    }

    public void HandleAnimalMovedEvent(AnimalMovedEvent animalMovedEvent)
    {
      _logger.LogInformation(
          "Event handled: Animal {AnimalId} moved from enclosure {SourceId} to enclosure {DestinationId}",
          animalMovedEvent.AnimalId,
          animalMovedEvent.SourceEnclosureId,
          animalMovedEvent.DestinationEnclosureId);
    }

    public void HandleFeedingTimeEvent(FeedingTimeEvent feedingTimeEvent)
    {
      _logger.LogInformation(
          "Event handled: Animal {AnimalId} feeding time with food type {FoodType}",
          feedingTimeEvent.AnimalId,
          feedingTimeEvent.FoodType);
    }
  }
}
