using FinanceApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
  public class Operation
  {
    public Guid Id { get; private set; }
    public OperationType Type { get; private set; }

    public Guid BankAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public Guid CategoryId { get; private set; }

    public Operation(Guid id, OperationType type, Guid bankAccountId, decimal amount, DateTime date, string? description, Guid categoryId)
    {
      this.Id = id;
      this.Type = type;
      this.BankAccountId = bankAccountId;
      this.Amount = amount;
      this.Date = date;
      this.Description = description;
      this.CategoryId = categoryId;
    }

    public void UpdateType(OperationType type)
    {
      Type = type;
    }
    public void UpdateAmount(decimal amount)
    {
      Amount = amount;
    }

    public void UpdateDate(DateTime date)
    {
      Date = date;
    }
    public void UpdateCategoryId(Guid categoryId)
    {
      CategoryId = categoryId;
    }
    public void UpdateDescription(string description)
    {
      Description = description;
    }
  }

}
