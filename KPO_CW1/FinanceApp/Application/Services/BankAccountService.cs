using FinanceApp.Domain.Interfaces.IFactory;
using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models;
using System;

namespace FinanceApp.Application.Services
{
  public class BankAccountService : IBankAccountService
  {
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IDomainModelFactory _domainModelFactory;

    public BankAccountService(
        IBankAccountRepository bankAccountRepository,
        IOperationRepository operationRepository,
        IDomainModelFactory domainModelFactory)
    {
      _bankAccountRepository = bankAccountRepository;
      _operationRepository = operationRepository;
      _domainModelFactory = domainModelFactory;
    }


    public BankAccount GetById(Guid id)
    {
      return _bankAccountRepository.GetById(id);
    }

    public IEnumerable<BankAccount> GetAll()
    {
      return _bankAccountRepository.GetAll();
    }

    public BankAccount Create(string name, decimal initialBalance)
    {
      var bankAccount = _domainModelFactory.CreateBankAccount(name, initialBalance);
      _bankAccountRepository.Add(bankAccount);
      return bankAccount;
    }

    public void Update(Guid id, string name)
    {
      var bankAccount = _bankAccountRepository.GetById(id);
      if (bankAccount == null)
        throw new ArgumentException($"Bank account with ID {id} not found");

      bankAccount.UpdateName(name);
      _bankAccountRepository.Update(bankAccount);
    }

    public void Delete(Guid id)
    {
      var bankAccount = _bankAccountRepository.GetById(id);
      if (bankAccount == null)
        throw new ArgumentException($"Bank account with ID {id} not found");

      var operations = _operationRepository.GetByBankAccount(id);
      foreach (var operation in operations)
      {
        _operationRepository.Delete(operation.Id);
      }
      _bankAccountRepository.Delete(id);
    }
  }
}
