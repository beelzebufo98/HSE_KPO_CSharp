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
  public class EditOperationCommand : CommandBase
  {
    private readonly IOperationService _operationService;
    private readonly Guid _id;
    private readonly OperationType _type;
    private readonly decimal _amount;
    private readonly DateTime _date;
    private readonly string _description;
    private readonly Guid _categoryId;

    public EditOperationCommand(
        IOperationService operationService,
        Guid id,
        OperationType type,
        decimal amount,
        DateTime date,
        string description,
        Guid categoryId)
    {
      _operationService = operationService;
      _id = id;
      _type = type;
      _amount = amount;
      _date = date;
      _description = description;
      _categoryId = categoryId;
    }

    public override void Execute()
    {
      _operationService.Update(_id, _type, _amount, _date, _description, _categoryId);
    }
  }
}
