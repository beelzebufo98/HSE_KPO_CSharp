using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.OperationCommands
{
  public class CreateOperationCommand : CommandBase
  {
    private readonly IOperationService _operationService;
    private readonly OperationType _type;
    private readonly Guid _bankAccountId;
    private readonly decimal _amount;
    private readonly DateTime _date;
    private readonly string _description;
    private readonly Guid _categoryId;

    public CreateOperationCommand(
        IOperationService operationService,
        OperationType type,
        Guid bankAccountId,
        decimal amount,
        DateTime date,
        string description,
        Guid categoryId)
    {
      _operationService = operationService;
      _type = type;
      _bankAccountId = bankAccountId;
      _amount = amount;
      _date = date;
      _description = description;
      _categoryId = categoryId;
    }

    public override void Execute()
    {
      _operationService.Create(_type, _bankAccountId, _amount, _date, _description, _categoryId);
    }
  }
}
