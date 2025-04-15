using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IRepository
{
  public interface IBankAccountRepository
  {
    BankAccount GetById(Guid id);
    IEnumerable<BankAccount> GetAll();
    void Add(BankAccount bankAccount);
    void Update(BankAccount bankAccount);
    void Delete(Guid id);
  }
}
