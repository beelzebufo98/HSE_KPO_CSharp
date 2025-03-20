using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.BankAccountCommands
{
  public class DeleteBankAccountCommand : CommandBase
  {
    private readonly IBankAccountService _bankAccountService;
    private readonly Guid _id;

    public DeleteBankAccountCommand(IBankAccountService bankAccountService, Guid id)
    {
      _bankAccountService = bankAccountService;
      _id = id;
    }

    public override void Execute()
    {
      _bankAccountService.Delete(_id);
    }
  }
}
