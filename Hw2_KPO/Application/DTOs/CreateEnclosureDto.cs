using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.DTOs
{
  public record CreateEnclosureDto(
    string Type,
    int Capacity
);
}
