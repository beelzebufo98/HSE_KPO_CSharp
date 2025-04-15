using Hw2_KPO.Application.DTOs;
using Hw2_KPO.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hw2_KPO.Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StatisticsController : ControllerBase
  {
    private readonly ZooStatisticsService _statisticsService;
    private readonly ILogger<StatisticsController> _logger;

    public StatisticsController(ZooStatisticsService statisticsService, ILogger<StatisticsController> logger)
    {
      _statisticsService = statisticsService;
      _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ZooStatisticsDto>> GetZooStatistics()
    {
      var statistics = _statisticsService.GetZooStatistics();
      return Ok(statistics);
    }
  }

}
