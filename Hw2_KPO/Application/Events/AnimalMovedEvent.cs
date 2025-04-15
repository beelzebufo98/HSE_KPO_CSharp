using Hw2_KPO.Domain.Interfaces;

namespace Hw2_KPO.Application.Events
{
  public class AnimalMovedEvent:DomainEvent
  {
    public Guid AnimalId { get; }
    public Guid SourceEnclosureId { get; }
    public Guid DestinationEnclosureId { get; }

    public AnimalMovedEvent(Guid animalId, Guid sourceEnclosureId, Guid destinationEnclosureId)
    {
      AnimalId = animalId;
      SourceEnclosureId = sourceEnclosureId;
      DestinationEnclosureId = destinationEnclosureId;
    }
  }
}
