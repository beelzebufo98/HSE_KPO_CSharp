using Hw2_KPO.Application.DTOs;
using Hw2_KPO.Application.Services;
using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Hw2_KPO.Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AnimalsController : ControllerBase
  {
    private readonly IAnimalRepository _animalRepository;
    private readonly AnimalTransferService _transferService;
    private readonly ILogger<AnimalsController> _animalLogger;

    public AnimalsController(IAnimalRepository animalRepository,
      AnimalTransferService transferService,
      ILogger<AnimalsController> animalLogger)
    {
      _animalRepository = animalRepository;
      _transferService = transferService;
      _animalLogger = animalLogger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<object>>> GetAllAnimals()
    {
      var animals = _animalRepository.GetAll();
      var animalDtos = new List<object>();

      foreach (var animal in animals)
      {
        animalDtos.Add(new
        {
          Id = animal.Id,
          Type = animal.Type.ToString(),
          Name = animal.Name,
          BirthDate = animal.Date,
          Gender = animal.Gender,
          FavoriteFood = animal.FavorFood.ToString(),
          IsHealthy = animal.isHealthy,
          IsHappily = animal.isHappily
        });
      }

      _animalLogger.LogInformation("All animals retrieved.");
      return Ok(animalDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetAnimal(Guid id)
    {
      var animal = _animalRepository.GetById(id);
      if (animal == null)
      {
        _animalLogger.LogError($"Animal with id {id} not found.");
        return NotFound();
      }

      var animalDto = new
      {
        Id = animal.Id,
        Type = animal.Type.ToString(),
        Name = animal.Name,
        BirthDate = animal.Date,
        Gender = animal.Gender,
        FavoriteFood = animal.FavorFood.ToString(),
        IsHealthy = animal.isHealthy,
        IsHappily = animal.isHappily
      };

      _animalLogger.LogInformation($"Animal with id {id} found.");
      return Ok(animalDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AnimalDto>> CreateAnimal([FromBody] CreateAnimalDto createDto)
    {
      if (!Enum.TryParse<AnimalType>(createDto.Type, true, out var animalType))
      {
        _animalLogger.LogError($"Invalid animal type: {createDto.Type}");
        return BadRequest($"Invalid animal type: {createDto.Type}");
      }

      if (!Enum.TryParse<FoodType>(createDto.FavorFood, true, out var foodType))
      {
        _animalLogger.LogError($"Invalid food type: {createDto.FavorFood}");  
        return BadRequest($"Invalid food type: {createDto.FavorFood}");
      }
      var animal = new Animal(
        animalType,
        createDto.Name,
        createDto.BirthDate,
        createDto.Gender,
        foodType
        );
      _animalRepository.AddAnimal(animal);
      var animalDto = new AnimalDto(
            animal.Id,
            animal.Type.ToString(),
            animal.Name,
            animal.Date,
            animal.Gender,
            animal.FavorFood.ToString(),
            animal.isHealthy,
            animal.isHappily
        );
      return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animalDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAnimal(Guid id)
    {
      var animal = _animalRepository.GetById(id);
      if (animal == null)
      {
        _animalLogger.LogError($"Animal with id {id} not found.");
        return NotFound();
      }
      _animalRepository.RemoveAnimal(id);
      _animalLogger.LogInformation($"Animal with id {id} deleted.");
      return NoContent();
    }


    [HttpPut("{id}/health/setill")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetIllAnimal(Guid id)
    {
      var animal = _animalRepository.GetById(id);
      if (animal == null)
      {
        _animalLogger.LogError($"Animal with id {id} not found.");
        return NotFound();
      }
      animal.SetSick();
      _animalRepository.Update(animal);
      _animalLogger.LogInformation($"Animal with id {id} set to sick.");
      return Ok();
    }

    [HttpPut("{id}/health/heal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HealAnimal(Guid id)
    {
      var animal =  _animalRepository.GetById(id);
      if (animal == null)
      {
        _animalLogger.LogError($"Animal with id {id} not found.");
        return NotFound();
      }

      animal.Heal();
      _animalRepository.Update(animal);
      _animalLogger.LogInformation($"Animal with id {id} healed.");
      return Ok();
    }

    [HttpPost("{id}/transfer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TransferAnimal(Guid id, [FromBody] Guid enclosureId)
    {
      try
      {
        _animalLogger.LogInformation($"Transferring animal with id {id} to enclosure with id {enclosureId}.");
        _transferService.MoveAnimal(id, enclosureId);
        _animalLogger.LogInformation($"Animal with id {id} transferred to enclosure with id {enclosureId}.");
        return Ok();
      }
      catch(Exception ex)
      {
        _animalLogger.LogError($"Error transferring animal with id {id}: {ex.Message}");
        return BadRequest(ex.Message);
      }
    }
  }
}
