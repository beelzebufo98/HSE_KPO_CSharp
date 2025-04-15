using Hw2_KPO.Application.Events;
using Hw2_KPO.Domain.Interfaces;

namespace Hw2_KPO.Application.Services
{
  public class AnimalTransferService
  {
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IDomainEventHandler<AnimalMovedEvent> _eventHandler;
    private readonly ILogger<AnimalTransferService> _logger;

    public AnimalTransferService(IAnimalRepository animalRepository,
      IEnclosureRepository enclosureRepository,
      IDomainEventHandler<AnimalMovedEvent> eventHandler,
      ILogger<AnimalTransferService> logger)
    {
      _animalRepository = animalRepository;
      _enclosureRepository = enclosureRepository;
      _eventHandler = eventHandler;
      _logger = logger;
    }
    public void MoveAnimal(Guid animalId, Guid enclId)
    {
      var animal = _animalRepository.GetById(animalId);
      if (animal == null)
      {
        _logger.LogWarning($"There is no animal with an id = {animalId}");
        throw new Exception();

      }
      var curEncId = animal.enclosureId;
      if (enclId == Guid.Empty)
      {
        _logger.LogWarning($"There is no enclosure with an id = {animalId}, so the animal won't be move");
        throw new Exception();

      }
      var moveEnc = _enclosureRepository.GetById(enclId);
      _logger.LogDebug($"moveEnc is null: {moveEnc == null}");
      if (curEncId != Guid.Empty)
      {
        var delEnc = _enclosureRepository.GetById(curEncId);
        if (delEnc == null)
        {
          var resBool = moveEnc.AddAnimal(animalId, animal.Type);
          if (resBool)
          {
            _logger.LogInformation($"The animal was added to the enclosure with the id = {enclId}");
            var animalMovedEvent = new AnimalMovedEvent(
                  animalId,
                  curEncId,
                  enclId);

            _eventHandler.Handler(animalMovedEvent);
            return;
          }
        }
        else if (moveEnc != null && moveEnc.CurrentSize < moveEnc.Size)
        {
          var resBool = moveEnc.AddAnimal(animalId, animal.Type);
          if (resBool)
          {
            _logger.LogInformation($"The animal was added to the enclosure with the id = {enclId}");
            delEnc.RemoveAnimal(animalId);
            _logger.LogInformation($"The animal was removed from the enclosure with the id= {curEncId}");
            var animalMovedEvent = new AnimalMovedEvent(
                  animalId,
                  curEncId,
                  enclId);

            _eventHandler.Handler(animalMovedEvent);
            return;
          }
        }
        _logger.LogInformation($"The animal was not added to the enclosure with the id = {enclId}");
        return;
      }
      else
      {
        if (moveEnc != null)
        {
          var resBool =  moveEnc.AddAnimal(animalId, animal.Type);
          if (resBool)
          {
            animal.enclosureId = moveEnc.Id;
            _logger.LogInformation($"The animal was added to the enclosure with the id = {enclId}");
            var animalMovedEvent = new AnimalMovedEvent(
                   animalId,
                   curEncId,
                   enclId);

            _eventHandler.Handler(animalMovedEvent);
            return;
          }
          _logger.LogInformation($"The animal was not added to the enclosure with the id = {enclId}");
          return;
        }
      }
    }
  }
}
