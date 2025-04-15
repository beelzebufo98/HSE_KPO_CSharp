using FinanceApp.Application.Commands.OperationCommands;
using FinanceApp.Application.Decorators;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using FinanceApp.Application.Commands.Base;

namespace FinanceApp.Application.Facades
{
  public class OperationFacade
  {
    private readonly IOperationService _operationService;

    public OperationFacade(IOperationService operationService)
    {
      _operationService = operationService;
    }

    public IEnumerable<Operation> GetAll()
    {
      return _operationService.GetAll();
    }

    public Operation GetById(Guid id)
    {
      return _operationService.GetById(id);
    }

    public IEnumerable<Operation> GetByBankAccount(Guid bankAccountId)
    {
      return _operationService.GetByBankAccount(bankAccountId);
    }

    public IEnumerable<Operation> GetByCategory(Guid categoryId)
    {
      return _operationService.GetByCategory(categoryId);
    }

    public IEnumerable<Operation> GetByDateRange(DateTime startDate, DateTime endDate)
    {
      return _operationService.GetByDateRange(startDate, endDate);
    }

    public void CreateOperation(
        OperationType type,
        Guid bankAccountId,
        decimal amount,
        DateTime date,
        string description,
        Guid categoryId,
        bool measureTime = false)
    {
      ICommand command = new CreateOperationCommand(
          _operationService, type, bankAccountId, amount, date, description, categoryId);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Create Operation");

      command.Execute();
    }

    public void UpdateOperation(
        Guid id,
        OperationType type,
        decimal amount,
        DateTime date,
        string description,
        Guid categoryId,
        bool measureTime = false)
    {
      ICommand command = new EditOperationCommand(
          _operationService, id, type, amount, date, description, categoryId);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Update Operation");

      command.Execute();
    }

    public void DeleteOperation(Guid id, bool measureTime = false)
    {
      ICommand command = new DeleteOperationCommand(_operationService, id);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Delete Operation");

      command.Execute();
    }
  }
}
