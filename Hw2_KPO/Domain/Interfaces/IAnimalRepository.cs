using Hw2_KPO.Domain.Entities;

namespace Hw2_KPO.Domain.Interfaces
{
  public interface IAnimalRepository
  {
    public Animal GetById(Guid id);
    public void Update(Animal animal);
    public IEnumerable<Animal> GetAll();
    public void AddAnimal(Animal animal);
    public void RemoveAnimal(Guid id);
  }
}
