using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
  public class AnalyticsService : IAnalyticsService
  {
    private readonly IOperationRepository _operationRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AnalyticsService(IOperationRepository operationRepository, ICategoryRepository categoryRepository)
    {
      _operationRepository = operationRepository;
      _categoryRepository = categoryRepository;
    }

    public decimal CalculateIncomeExpenseDifference(DateTime startDate, DateTime endDate)
    {
      var operations = _operationRepository.GetByDateRange(startDate, endDate);

      decimal income = operations
          .Where(o => o.Type == OperationType.Income)
          .Sum(o => o.Amount);

      decimal expense = operations
          .Where(o => o.Type == OperationType.Expense)
          .Sum(o => o.Amount);

      return income + expense;
    }

    public Dictionary<Category, decimal> GroupOperationsByCategory(DateTime startDate, DateTime endDate, OperationType type)
    {
      var operations = _operationRepository.GetByDateRange(startDate, endDate)
          .Where(o => o.Type == type);

      var result = new Dictionary<Category, decimal>();

      foreach (var operation in operations)
      {
        var category = _categoryRepository.GetById(operation.CategoryId);
        if (category != null)
        {
          if (result.ContainsKey(category))
            result[category] += operation.Amount;
          else
            result[category] = operation.Amount;
        }
      }

      return result;
    }
  }
}
