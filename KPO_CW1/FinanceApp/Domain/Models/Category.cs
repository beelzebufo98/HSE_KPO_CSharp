using FinanceApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
  public class Category
  {
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public OperationType Type {  get; private set; }
    public Category(Guid id, string name, OperationType type)
    {
      this.Id = id;
      this.Name = name;
      this.Type = type;
    }

    internal void UpdateName(string name)
    {
      Name = name;
    }

    internal void UpdateType(OperationType type)
    {
      Type = type;
    }
  }
}
