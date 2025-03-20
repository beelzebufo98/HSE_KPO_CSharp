using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IRepository
{
  public interface IOperationRepository
  {
    Operation GetById(Guid id);
    IEnumerable<Operation> GetAll();
    IEnumerable<Operation> GetByBankAccount(Guid bankAccountId);
    IEnumerable<Operation> GetByCategory(Guid categoryId);
    IEnumerable<Operation> GetByDateRange(DateTime startDate, DateTime endDate);
    void Add(Operation operation);
    void Update(Operation operation);
    void Delete(Guid id);
  }
}
