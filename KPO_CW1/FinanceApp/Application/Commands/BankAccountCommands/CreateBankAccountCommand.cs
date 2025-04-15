using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.BankAccountCommands
{
  public class CreateBankAccountCommand : CommandBase
  {
    private readonly IBankAccountService _bankAccountService;
    private readonly string _name;
    private readonly decimal _initialBalance;

    public CreateBankAccountCommand(IBankAccountService bankAccountService, string name, decimal initialBalance)
    {
      _bankAccountService = bankAccountService;
      _name = name;
      _initialBalance = initialBalance;
    }

    public override void Execute()
    {
      _bankAccountService.Create(_name, _initialBalance);
    }
  }
}
