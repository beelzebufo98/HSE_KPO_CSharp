using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;

namespace FinanceApp.Application.Facades
{
  public class AnalyticsFacade
  {
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsFacade(IAnalyticsService analyticsService)
    {
      _analyticsService = analyticsService;
    }

    public decimal CalculateIncomeExpenseDifference(DateTime startDate, DateTime endDate, bool measureTime = false)
    {
      if (!measureTime)
        return _analyticsService.CalculateIncomeExpenseDifference(startDate, endDate);

      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();
      var result = _analyticsService.CalculateIncomeExpenseDifference(startDate, endDate);
      stopwatch.Stop();
      Console.WriteLine($"Income-Expense difference calculated in {stopwatch.ElapsedMilliseconds} ms");
      return result;
    }

    public Dictionary<Category, decimal> GroupOperationsByCategory(
        DateTime startDate,
        DateTime endDate,
        OperationType type,
        bool measureTime = false)
    {
      if (!measureTime)
        return _analyticsService.GroupOperationsByCategory(startDate, endDate, type);

      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();
      var result = _analyticsService.GroupOperationsByCategory(startDate, endDate, type);
      stopwatch.Stop();
      Console.WriteLine($"Operations grouped by category in {stopwatch.ElapsedMilliseconds} ms");
      return result;
    }
  }
}
