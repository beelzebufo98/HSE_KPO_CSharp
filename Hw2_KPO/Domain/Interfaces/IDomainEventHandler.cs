using Hw2_KPO.Application.Events;

namespace Hw2_KPO.Domain.Interfaces
{
  public interface IDomainEventHandler<T> where T : DomainEvent
  {
    void Handler(T domainEvent);
  }
}
