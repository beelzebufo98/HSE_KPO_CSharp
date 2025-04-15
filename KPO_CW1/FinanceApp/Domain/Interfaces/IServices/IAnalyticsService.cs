using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IServices
{
  public interface IAnalyticsService
  {
    decimal CalculateIncomeExpenseDifference(DateTime startDate, DateTime endDate);
    Dictionary<Category, decimal> GroupOperationsByCategory(DateTime startDate, DateTime endDate, OperationType type);
  }
}
