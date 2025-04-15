using Hw2_KPO.Application.DTOs;
using Hw2_KPO.Domain.Entities;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Hw2_KPO.Presentation.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EnclosuresController:ControllerBase
  {
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly ILogger<EnclosuresController> _logger;
    //private readonly IAnimalRepository _animalRepository;
    public EnclosuresController(IEnclosureRepository enclosureRepository, 
      ILogger<EnclosuresController> logger)
    {
      _enclosureRepository = enclosureRepository;
      _logger = logger;
      
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EnclosureDto>>> GetAllEnclosures()
    {
      var enclosures = _enclosureRepository.GetAll();
      var enclosuresDtos = new List<EnclosureDto>();

      foreach(var enclosure in enclosures)
      {
        enclosuresDtos.Add(new EnclosureDto(enclosure.Id,
          enclosure.EncType.ToString(),
          enclosure.Size,
          enclosure.CurrentSize,
          enclosure.AnimalsIds));
      }
      return Ok(enclosuresDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnclosureDto>> GetEnclosure(Guid id)
    {
      var enclosure = _enclosureRepository.GetById(id);
      if (enclosure == null)
      {
        return NotFound();
      }
      var enclosureDto = new EnclosureDto(enclosure.Id,
        enclosure.EncType.ToString(),
        enclosure.Size,
        enclosure.CurrentSize,
        enclosure.AnimalsIds);
      return Ok(enclosureDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EnclosureDto>> CreateEnclosure([FromBody] CreateEnclosureDto enclosureDto)
    {
       if (!Enum.TryParse<EnclosureType>(enclosureDto.Type, true, out var enclosureType))
      {
        _logger.LogError("Invalid enclosure type: {Type}", enclosureDto.Type);
        return BadRequest("Invalid enclosure type");
      }
      var enclosure = new Enclosure(enclosureType, enclosureDto.Capacity);
      _enclosureRepository.AddEnclosure(enclosure);
      var createdEnclosureDto = new EnclosureDto(enclosure.Id,
        enclosure.EncType.ToString(),
        enclosure.Size,
        enclosure.CurrentSize,
        enclosure.AnimalsIds);
      _logger.LogInformation("Enclosure created with id: {Id}", enclosure.Id);
      return CreatedAtAction(nameof(GetEnclosure), new { id = enclosure.Id }, createdEnclosureDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEnclosure(Guid id)
    {
      var enclosure = _enclosureRepository.GetById(id);
      if (enclosure == null)
      {
        _logger.LogWarning("Enclosure with id {Id} not found", id);
        return NotFound();
      }
      if (enclosure.CurrentSize > 0)
      {
        return BadRequest("Cannot delete an enclosure that contains animals");
      }
      //var animals = _animalRepository.GetAll(); //TODO: посмотреть
      //foreach (var animal in animals)
      //{
      //  animal.enclosureId = Guid.Empty;
      //  _animalRepository.Update(animal);
      //}
      _enclosureRepository.RemoveEnclosure(id);
      _logger.LogInformation("Enclosure deleted with id: {Id}", id);
      return NoContent();
    }

    [HttpPost("{id}/clean")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CleanEnclosure(Guid id)
    {
      var enclosure = _enclosureRepository.GetById(id);
      if (enclosure == null)
      {
        _logger.LogWarning("Enclosure with id {Id} not found", id);
        return NotFound();
      }
      enclosure.Clean();
      //var animals = _animalRepository.GetAll(); //TODO: посмотреть
      //foreach (var animal in animals)
      //{
      //  animal.enclosureId = Guid.Empty;
      //  _animalRepository.Update(animal);
      //}
      _enclosureRepository.Update(enclosure);
      _logger.LogInformation("Enclosure cleaned with id: {Id}", id);
      return Ok();
    }

    [HttpGet("{id}/animals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAnimalsInEnclosure(Guid id)
    {
      var enclosure = _enclosureRepository.GetById(id);
      if (enclosure == null)
      {
        _logger.LogWarning("Enclosure with id {Id} not found", id);
        return NotFound();
      }
      var animals = _enclosureRepository.GetAnimalsInEnclosure(id);
      var animalDtos = new List<AnimalDto>();
      foreach (var animal in animals)
      {
        animalDtos.Add(new AnimalDto(
         animal.Id,
         animal.Type.ToString(),
         animal.Name,
          animal.Date,
          animal.Gender,
          animal.FavorFood.ToString(),
          animal.isHealthy,
          animal.isHappily
          ));
      }
      _logger.LogInformation("Animals in enclosure with id {Id} retrieved", id);
      return Ok(animalDtos);
    }
  }
}
