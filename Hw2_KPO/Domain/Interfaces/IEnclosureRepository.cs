using Hw2_KPO.Domain.Entities;

namespace Hw2_KPO.Domain.Interfaces
{
  public interface IEnclosureRepository
  {
    public Enclosure GetById(Guid id);
    public void Update(Enclosure enclosure);
    public IEnumerable<Animal> GetAnimalsInEnclosure(Guid id);
    public IEnumerable<Enclosure> GetAll();
    public void AddEnclosure(Enclosure enclosure);
    public void RemoveEnclosure(Guid id);
  }
}
