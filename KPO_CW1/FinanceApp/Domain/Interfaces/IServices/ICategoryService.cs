using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IServices
{
  public interface ICategoryService
  {
    Category GetById(Guid id);
    IEnumerable<Category> GetAll();
    IEnumerable<Category> GetByType(OperationType type);
    Category Create(OperationType type, string name);
    void Update(Guid id, OperationType type, string name);
    void Delete(Guid id);
  }
}
