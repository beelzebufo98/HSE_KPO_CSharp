using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IFactory
{
  public interface IDomainModelFactory
  {
    BankAccount CreateBankAccount(string name, decimal initialBalance);
    Category CreateCategory(OperationType type, string name);
    Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount,
                            DateTime date, string description, Guid categoryId);
  }
}
