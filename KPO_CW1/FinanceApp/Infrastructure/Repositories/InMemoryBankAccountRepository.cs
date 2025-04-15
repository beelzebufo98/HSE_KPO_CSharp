using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
  public class InMemoryBankAccountRepository : IBankAccountRepository
  {
    private readonly Dictionary<Guid, BankAccount> _bankAccounts = new Dictionary<Guid, BankAccount>();

    public BankAccount GetById(Guid id)
    {
      return _bankAccounts.TryGetValue(id, out var bankAccount) ? bankAccount : null;
    }

    public IEnumerable<BankAccount> GetAll()
    {
      return _bankAccounts.Values;
    }

    public void Add(BankAccount bankAccount)
    {
      _bankAccounts[bankAccount.Id] = bankAccount;
    }

    public void Update(BankAccount bankAccount)
    {
      _bankAccounts[bankAccount.Id] = bankAccount;
    }

    public void Delete(Guid id)
    {
      if (_bankAccounts.ContainsKey(id))
        _bankAccounts.Remove(id);
    }
  }

}
