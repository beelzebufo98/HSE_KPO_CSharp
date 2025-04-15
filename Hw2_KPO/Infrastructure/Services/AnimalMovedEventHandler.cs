using Hw2_KPO.Application.Events;
using Hw2_KPO.Domain.Interfaces;

namespace Hw2_KPO.Infrastructure.Services
{
  public class AnimalMovedEventHandler : IDomainEventHandler<AnimalMovedEvent>
  {
    private readonly EventHandlerService _eventHandlerService;

    public AnimalMovedEventHandler(EventHandlerService eventHandlerService)
    {
      _eventHandlerService = eventHandlerService;
    }

    public void Handler(AnimalMovedEvent domainEvent)
    {
      _eventHandlerService.HandleAnimalMovedEvent(domainEvent);
    }
  }
}

