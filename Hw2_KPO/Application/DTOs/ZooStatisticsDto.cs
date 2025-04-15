namespace Hw2_KPO.Application.DTOs
{
  public class ZooStatisticsDto
  {
    public int TotalAnimals { get; set; }
    public int HealthyAnimals { get; set; }
    public int SickAnimals { get; set; }
    public int TotalEnclosures { get; set; }
    public int AvailableEnclosures { get; set; }
    public int FullEnclosures { get; set; }
    public Dictionary<string, int> AnimalsByType { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, int> EnclosuresByType { get; set; } = new Dictionary<string, int>();
  }
}
