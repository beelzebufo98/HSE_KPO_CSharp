using FinanceApp.Domain.Interfaces.IFactory;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using FinanceApp.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Factories
{
  public class DomainModelFactory : IDomainModelFactory
  {
    public BankAccount CreateBankAccount(string name, decimal initialBalance)
    {
      DomainModelValidator.ValidateBankAccount(name, initialBalance);
      return new BankAccount(Guid.NewGuid(), name, initialBalance);
    }

    public Category CreateCategory(OperationType type, string name)
    {
      DomainModelValidator.ValidateCategory(type, name);
      return new Category(Guid.NewGuid(), name, type);
    }

    public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount,
                                   DateTime date, string description, Guid categoryId)
    {
     
      decimal adjustedAmount = type == OperationType.Expense ? -Math.Abs(amount) : Math.Abs(amount);

      DomainModelValidator.ValidateOperation(type, bankAccountId, adjustedAmount, date, categoryId);
      return new Operation(Guid.NewGuid(), type, bankAccountId, adjustedAmount, date, description, categoryId);
    }
  }
}
