using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.OperationCommands
{
  public class DeleteOperationCommand : CommandBase
  {
    private readonly IOperationService _operationService;
    private readonly Guid _id;

    public DeleteOperationCommand(IOperationService operationService, Guid id)
    {
      _operationService = operationService;
      _id = id;
    }

    public override void Execute()
    {
      _operationService.Delete(_id);
    }
  }
}
