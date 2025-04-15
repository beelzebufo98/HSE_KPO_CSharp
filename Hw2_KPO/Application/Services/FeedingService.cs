using Hw2_KPO.Application.Events;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.Services
{
  public class FeedingService
  {
    private readonly IAnimalRepository _animalRepository;
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IDomainEventHandler<FeedingTimeEvent> _eventHandler;
    private readonly ILogger<FeedingService> _logger;

    public FeedingService(IAnimalRepository animalRepository,
      IFeedingScheduleRepository feedingScheduleRepository,
      IDomainEventHandler<FeedingTimeEvent> eventHandler,
      ILogger<FeedingService> logger)
    {
      _animalRepository = animalRepository;
      _feedingScheduleRepository = feedingScheduleRepository;
      _eventHandler = eventHandler;
      _logger = logger;
    }

    public void ExecuteFeeding(Guid animalId, FoodType foodType)
    {
      var animal = _animalRepository.GetById(animalId);
      if (animal == null)
      {
        _logger.LogWarning("Animal with ID {AnimalId} not found", animalId);
      }
      animal.Feed(foodType);
      _logger.LogInformation("Animal with ID {AnimalId} has eaten with {FoodType} and is satisfied", animalId, foodType);
      var feedingEvent = new FeedingTimeEvent(animalId, foodType);
      _eventHandler.Handler(feedingEvent);
    }

    public void ProcessScheduledFeedings()
    {
      var now = DateTime.Now;
      var schedules = _feedingScheduleRepository.GetAll();
      var activeSchedules = schedules.Where(schedule => schedule.IsActive).
        Where(schedule => Math.Abs((now.TimeOfDay - schedule.Time).TotalMinutes) < 20).ToList();
      foreach (var schedule in activeSchedules)
      {
        var animals = _animalRepository.GetAll();
        var matchingAnimals = animals.Where(a => a.Type == schedule.TypeAnimal);
        foreach (var animal in matchingAnimals)
        {
          ExecuteFeeding(animal.Id, schedule.FType);
        }
      }
    }
  }
}
