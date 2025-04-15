using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.DTOs
{
  public record CreateAnimalDto(
      string Type,
      string Name,
      DateTime BirthDate,
      string Gender,
      string FavorFood
  );
}
