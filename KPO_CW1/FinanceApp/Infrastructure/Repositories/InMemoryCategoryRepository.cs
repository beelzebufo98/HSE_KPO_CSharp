using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
  public class InMemoryCategoryRepository : ICategoryRepository
  {
    private readonly Dictionary<Guid, Category> _categories = new Dictionary<Guid, Category>();

    public Category GetById(Guid id)
    {
      return _categories.TryGetValue(id, out var category) ? category : null;
    }

    public IEnumerable<Category> GetAll()
    {
      return _categories.Values;
    }

    public IEnumerable<Category> GetByType(OperationType type)
    {
      return _categories.Values.Where(c => c.Type == type);
    }

    public void Add(Category category)
    {
      _categories[category.Id] = category;
    }

    public void Update(Category category)
    {
      _categories[category.Id] = category;
    }

    public void Delete(Guid id)
    {
      if (_categories.ContainsKey(id))
        _categories.Remove(id);
    }
  }
}
