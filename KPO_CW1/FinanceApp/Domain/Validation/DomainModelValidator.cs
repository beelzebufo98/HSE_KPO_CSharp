using System;
using FinanceApp.Domain.Models.Enums;

namespace FinanceApp.Domain.Validation
{
  public static class DomainModelValidator
  {
    public static void ValidateBankAccount(string name, decimal balance)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Bank account name cannot be empty", nameof(name));

      if (name.Length > 100)
        throw new ArgumentException("Bank account name cannot exceed 100 characters", nameof(name));
    }

    public static void ValidateCategory(OperationType type, string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Category name cannot be empty", nameof(name));

      if (name.Length > 50)
        throw new ArgumentException("Category name cannot exceed 50 characters", nameof(name));
    }

    public static void ValidateOperation(OperationType type, Guid bankAccountId, decimal amount,
                                      DateTime date, Guid categoryId)
    {
      if (bankAccountId == Guid.Empty)
        throw new ArgumentException("Bank account ID cannot be empty", nameof(bankAccountId));

      if (type == OperationType.Income && amount < 0)
        throw new ArgumentException("Income amount must be positive", nameof(amount));

      if (amount == 0)
        throw new ArgumentException("Operation amount cannot be zero", nameof(amount));

      if (date > DateTime.Now)
        throw new ArgumentException("Operation date cannot be in the future", nameof(date));

      if (categoryId == Guid.Empty)
        throw new ArgumentException("Category ID cannot be empty", nameof(categoryId));
    }
  }
}