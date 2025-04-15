using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
  public class BankAccount
  {
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public decimal Balance { get; private set; }

    public BankAccount(Guid id, string name, decimal balance)
    {
      Id = id;
      Name = name;
      Balance = balance;
    }

    public void UpdateName(string name)
    {
      Name = name;
    }

    public void UpdateBalance(decimal amount)
    {
      Balance += amount;
    }
  }
  
}
