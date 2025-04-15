using Hw2_KPO.Domain.ValueObjects;

namespace Hw2_KPO.Application.DTOs
{
  public record CreateFeedingScheduleDto(
     string AnimalType,
     TimeSpan FeedingTime,
     string FType
     
 );
}
