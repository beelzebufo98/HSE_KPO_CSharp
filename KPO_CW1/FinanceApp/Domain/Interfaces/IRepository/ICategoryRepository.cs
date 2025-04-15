using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.IRepository
{
  public interface ICategoryRepository
  {
    Category GetById(Guid id);
    IEnumerable<Category> GetAll();
    IEnumerable<Category> GetByType(OperationType type);
    void Add(Category category);
    void Update(Category category);
    void Delete(Guid id);
  }

}
