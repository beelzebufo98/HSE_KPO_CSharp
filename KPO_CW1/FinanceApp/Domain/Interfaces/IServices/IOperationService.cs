using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IServices
{
  public interface IOperationService
  {
    Operation GetById(Guid id);
    IEnumerable<Operation> GetAll();
    IEnumerable<Operation> GetByBankAccount(Guid bankAccountId);
    IEnumerable<Operation> GetByCategory(Guid categoryId);
    IEnumerable<Operation> GetByDateRange(DateTime startDate, DateTime endDate);
    Operation Create(OperationType type, Guid bankAccountId, decimal amount,
                    DateTime date, string description, Guid categoryId);
    void Update(Guid id, OperationType type, decimal amount,
              DateTime date, string description, Guid categoryId);
    void Delete(Guid id);
  }
}
