using Hw2_KPO.Application.DTOs;
using Hw2_KPO.Domain.Interfaces;

namespace Hw2_KPO.Application.Services
{
  public class ZooStatisticsService
  {
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly ILogger<ZooStatisticsService> _logger;

    public ZooStatisticsService(IAnimalRepository animalRepository, 
      IEnclosureRepository enclosureRepository, ILogger<ZooStatisticsService> logger)
    {
      _animalRepository = animalRepository;
      _enclosureRepository = enclosureRepository;
      _logger = logger;
    }

    public async Task<ZooStatisticsDto> GetZooStatistics()
    {
      var animals = _animalRepository.GetAll();
      var enclosures = _enclosureRepository.GetAll();

      var animalsByType = animals
          .GroupBy(a => a.Type)
          .ToDictionary(g => g.Key.ToString(), g => g.Count());

      var enclosuresByType = enclosures
          .GroupBy(e => e.EncType)
          .ToDictionary(g => g.Key.ToString(), g => g.Count());

      var statistics = new ZooStatisticsDto
      {
        TotalAnimals = animals.Count(),
        HealthyAnimals = animals.Count(a => a.isHealthy),
        SickAnimals = animals.Count(a => !a.isHealthy),
        TotalEnclosures = enclosures.Count(),
        AvailableEnclosures = enclosures.Count(e => e.CurrentSize<e.Size),
        FullEnclosures = enclosures.Count(e => e.CurrentSize==e.Size),
        AnimalsByType = animalsByType,
        EnclosuresByType = enclosuresByType
      };

      _logger.LogInformation("Generated zoo statistics: {TotalAnimals} animals in {TotalEnclosures} enclosures",
          statistics.TotalAnimals, statistics.TotalEnclosures);

      return statistics;
    }
  }
}
