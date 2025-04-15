using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.DTOs
{
  public record EnclosureDto(
    Guid Id,
    string Type,
    int Capacity,
    int CurrentSize,
    List<Guid> AnimalIds
);
}
