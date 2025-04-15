using Hw2_KPO.Application.DTOs;
using Hw2_KPO.Application.Services;
using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Hw2_KPO.Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FeedingSchedulesController : ControllerBase
  {
    private readonly IFeedingScheduleRepository _feedingSchedule;
    private readonly FeedingService _feedingService;
    private readonly ILogger<FeedingSchedulesController> _logger;

    public FeedingSchedulesController(IFeedingScheduleRepository feedingSchedule,
      FeedingService feedingService, 
      ILogger<FeedingSchedulesController> logger)
    {
      _feedingService = feedingService;
      _logger = logger;
      _feedingSchedule = feedingSchedule;
    }
    [HttpGet("schedules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<object>>> GetAllSchedules()
    {
      var schedules = _feedingSchedule.GetAll();
      var schedulesDtos = new List<object>();
      foreach (var schedule in schedules)
      {
        schedulesDtos.Add(new FeedingScheduleDto(
          schedule.Id,
          schedule.TypeAnimal.ToString(),
          schedule.FType.ToString(),
          schedule.Time,
          schedule.IsActive
          ));
      }
      return Ok(schedulesDtos);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnclosureDto>> GetSchedule(Guid id)
    {
      var schedule = _feedingSchedule.GetById(id);
      if (schedule == null)
      {
        return NotFound();
      }
      var scheduleDto = new FeedingScheduleDto(
        schedule.Id,
        schedule.TypeAnimal.ToString(),
        schedule.FType.ToString(),
        schedule.Time,
        schedule.IsActive
        );
      return Ok(scheduleDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateFeedingSchedule([FromBody] CreateFeedingScheduleDto scheduleDto)
    {
      if (!Enum.TryParse<FoodType>(scheduleDto.FType, true, out var schedulType))
      {
        _logger.LogError("Invalid schedule foodType type: {Type}", scheduleDto.FType);
        return BadRequest("Invalid schedule foodType");
      }
      if (!Enum.TryParse<AnimalType>(scheduleDto.AnimalType, true, out var animalType))
      {
        _logger.LogError("Invalid schedule AnimalType: {Type}", scheduleDto.AnimalType);
        return BadRequest("Invalid schedule AnimalType");
      }
      var schedule = new FeedingSchedule(animalType, scheduleDto.FeedingTime, schedulType);
      _feedingSchedule.AddFeedingSchedule(schedule);
      var createdScheduleDto = new FeedingScheduleDto(
        schedule.Id,
        schedule.TypeAnimal.ToString(),
        schedule.FType.ToString(),
        schedule.Time,
        schedule.IsActive
        );
      _logger.LogInformation($"Schedule created with id: {schedule.Id}");
      return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, createdScheduleDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(Guid id)
    {
      var schedule = _feedingSchedule.GetById(id);
      if (schedule == null)
      {
        _logger.LogError($"Schedule with id {id} not found.");
        return NotFound();
      }
      _feedingSchedule.RemoveFeedingSchedule(id);
      _logger.LogInformation($"Schedule with id {id} deleted.");
      return NoContent();
    }

    [HttpPost("feed/{animalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> FeedAnimal(Guid animalId, [FromBody] string foodTypeStr)
    {
      if (!Enum.TryParse<FoodType>(foodTypeStr, true, out var foodType))
      {
        return BadRequest($"Invalid food type: {foodTypeStr}");
      }

      try
      {
        _feedingService.ExecuteFeeding(animalId, foodType);
        return Ok();
      }
      catch (Exception ex)
      {
        return NotFound($"Animal with ID {animalId} not found");
      }
    }

    [HttpGet("feeding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FeedingScheduleDto>>> GetAllFeeding()
    {
      _feedingService.ProcessScheduledFeedings();
      return Ok();
    }

    //TODO: сделать контроллер для кормления и ещё один контроллер для удаления расписания кормления
    //TODO: тесты сделать
  }
}
