using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.BankAccountCommands
{
  public class EditBankAccountCommand : CommandBase
  {
    private readonly IBankAccountService _bankAccountService;
    private readonly Guid _id;
    private readonly string _name;

    public EditBankAccountCommand(IBankAccountService bankAccountService, Guid id, string name)
    {
      _bankAccountService = bankAccountService;
      _id = id;
      _name = name;
    }

    public override void Execute()
    {
      _bankAccountService.Update(_id, _name);
    }
  }

}
