using FinanceApp.Domain.Interfaces.IFactory;
using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;

namespace FinanceApp.Application.Services
{
  public class OperationService : IOperationService
  {
    private readonly IOperationRepository _operationRepository;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDomainModelFactory _domainModelFactory;

    public OperationService(
        IOperationRepository operationRepository,
        IBankAccountRepository bankAccountRepository,
        ICategoryRepository categoryRepository,
        IDomainModelFactory domainModelFactory)
    {
      _operationRepository = operationRepository;
      _bankAccountRepository = bankAccountRepository;
      _categoryRepository = categoryRepository;
      _domainModelFactory = domainModelFactory;
    }

    public Operation GetById(Guid id)
    {
      return _operationRepository.GetById(id);
    }

    public IEnumerable<Operation> GetAll()
    {
      return _operationRepository.GetAll();
    }

    public IEnumerable<Operation> GetByBankAccount(Guid bankAccountId)
    {
      return _operationRepository.GetByBankAccount(bankAccountId);
    }

    public IEnumerable<Operation> GetByCategory(Guid categoryId)
    {
      return _operationRepository.GetByCategory(categoryId);
    }

    public IEnumerable<Operation> GetByDateRange(DateTime startDate, DateTime endDate)
    {
      return _operationRepository.GetByDateRange(startDate, endDate);
    }

    public Operation Create(
       OperationType type,
       Guid bankAccountId,
       decimal amount,
       DateTime date,
       string description,
       Guid categoryId)
    {
      
      var bankAccount = _bankAccountRepository.GetById(bankAccountId);
      if (bankAccount == null)
        throw new ArgumentException($"Bank account with ID {bankAccountId} not found");

      var category = _categoryRepository.GetById(categoryId);
      if (category == null)
        throw new ArgumentException($"Category with ID {categoryId} not found");

      if (category.Type != type)
        throw new ArgumentException($"Category type does not match operation type");

      var operation = _domainModelFactory.CreateOperation(type, bankAccountId, amount, date, description, categoryId);

      _operationRepository.Add(operation);

      bankAccount.UpdateBalance(operation.Amount);
      _bankAccountRepository.Update(bankAccount);

      return operation;
    }

    public void Update(Guid id, OperationType type, decimal amount,
                     DateTime date, string description, Guid categoryId)
    {
      var operation = _operationRepository.GetById(id);
      if (operation == null)
        throw new ArgumentException($"Operation with ID {id} not found");

      var category = _categoryRepository.GetById(categoryId);
      if (category == null)
        throw new ArgumentException($"Category with ID {categoryId} not found");

      if (category.Type != type)
        throw new ArgumentException($"Category type does not match operation type");

      var bankAccount = _bankAccountRepository.GetById(operation.BankAccountId);
      if (bankAccount == null)
        throw new ArgumentException($"Bank account with ID {operation.BankAccountId} not found");

      decimal adjustedAmount = type == OperationType.Expense ? -Math.Abs(amount) : Math.Abs(amount);

      operation.UpdateType(type);
      operation.UpdateAmount(adjustedAmount);
      operation.UpdateDate(date);
      operation.UpdateDescription(description);
      operation.UpdateCategoryId(categoryId);
      _operationRepository.Update(operation);
    }

    public void Delete(Guid id)
    {
      var operation = _operationRepository.GetById(id);
      if (operation == null)
        throw new ArgumentException($"Operation with ID {id} not found");
      _operationRepository.Delete(id);
    }
  }
}
