namespace Hw2_KPO.Application.Events
{
  public abstract class DomainEvent
  {
    public Guid Id { get; }
    public DateTime OccurredOn { get; protected set; }

    protected DomainEvent()
    {
      OccurredOn = DateTime.Now;
    }
  }
}
