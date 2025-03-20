using FinanceApp.Application.Commands.BankAccountCommands;
using FinanceApp.Application.Commands.Base;
using FinanceApp.Application.Decorators;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models;
using System;

namespace FinanceApp.Application.Facades
{
  public class BankAccountFacade
  {
    private readonly IBankAccountService _bankAccountService;

    public BankAccountFacade(IBankAccountService bankAccountService)
    {
      _bankAccountService = bankAccountService;
    }

    public IEnumerable<BankAccount> GetAll()
    {
      return _bankAccountService.GetAll();
    }

    public BankAccount GetById(Guid id)
    {
      return _bankAccountService.GetById(id);
    }

    public void CreateBankAccount(string name, decimal initialBalance, bool measureTime = false)
    {
      ICommand command = new CreateBankAccountCommand(_bankAccountService, name, initialBalance);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Create Bank Account");

      command.Execute();
    }

    public void UpdateBankAccount(Guid id, string name, bool measureTime = false)
    {
      ICommand command = new EditBankAccountCommand(_bankAccountService, id, name);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Update Bank Account");

      command.Execute();
    }

    public void DeleteBankAccount(Guid id, bool measureTime = false)
    {
      ICommand command = new DeleteBankAccountCommand(_bankAccountService, id);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Delete Bank Account");

      command.Execute();
    }
  }
}
