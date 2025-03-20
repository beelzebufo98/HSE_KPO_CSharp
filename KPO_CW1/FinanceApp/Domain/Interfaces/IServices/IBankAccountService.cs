using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IServices
{
  public interface IBankAccountService
  {
    BankAccount GetById(Guid id);
    IEnumerable<BankAccount> GetAll();
    BankAccount Create(string name, decimal initialBalance);
    void Update(Guid id, string name);
    void Delete(Guid id);
  }
}
