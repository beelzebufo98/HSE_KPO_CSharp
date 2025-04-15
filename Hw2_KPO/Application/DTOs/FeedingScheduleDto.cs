using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.DTOs
{
  public record FeedingScheduleDto(
    Guid Id,
    string TypeAnimal,
    string FoodType,
    TimeSpan FeedingTime,
    bool IsActive
);
}
