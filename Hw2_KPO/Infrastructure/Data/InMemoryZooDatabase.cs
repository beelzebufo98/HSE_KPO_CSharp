using Hw2_KPO.Domain.Entities;

namespace Hw2_KPO.Infrastructure.Data
{
  public class InMemoryZooDatabase
  {
    public List<Animal> Animals { get; } = new List<Animal>();
    public List<Enclosure> Enclosures { get; } = new List<Enclosure>();
    public List<FeedingSchedule> FeedingSchedules { get; } = new List<FeedingSchedule>();
  }
}
