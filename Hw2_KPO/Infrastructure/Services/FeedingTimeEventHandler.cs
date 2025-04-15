using Hw2_KPO.Application.Events;
using Hw2_KPO.Domain.Interfaces;

namespace Hw2_KPO.Infrastructure.Services
{
  public class FeedingTimeEventHandler : IDomainEventHandler<FeedingTimeEvent>
  {
    private readonly EventHandlerService _eventHandlerService;

    public FeedingTimeEventHandler(EventHandlerService eventHandlerService)
    {
      _eventHandlerService = eventHandlerService;
    }
    public void Handler(FeedingTimeEvent domainEvent)
    {
      _eventHandlerService.HandleFeedingTimeEvent(domainEvent);
    }
  }
}
