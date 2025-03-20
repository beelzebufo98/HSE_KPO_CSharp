using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
  public class InMemoryOperationRepository : IOperationRepository
  {
    private readonly Dictionary<Guid, Operation> _operations = new Dictionary<Guid, Operation>();

    public Operation GetById(Guid id)
    {
      return _operations.TryGetValue(id, out var operation) ? operation : null;
    }

    public IEnumerable<Operation> GetAll()
    {
      return _operations.Values;
    }

    public IEnumerable<Operation> GetByBankAccount(Guid bankAccountId)
    {
      return _operations.Values.Where(o => o.BankAccountId == bankAccountId);
    }

    public IEnumerable<Operation> GetByCategory(Guid categoryId)
    {
      return _operations.Values.Where(o => o.CategoryId == categoryId);
    }

    public IEnumerable<Operation> GetByDateRange(DateTime startDate, DateTime endDate)
    {
      return _operations.Values.Where(o => o.Date >= startDate && o.Date <= endDate);
    }

    public void Add(Operation operation)
    {
      _operations[operation.Id] = operation;
    }

    public void Update(Operation operation)
    {
      _operations[operation.Id] = operation;
    }

    public void Delete(Guid id)
    {
      if (_operations.ContainsKey(id))
        _operations.Remove(id);
    }
  }
}
