using FinanceApp.Application.Commands.CategoryCommands;
using FinanceApp.Application.Decorators;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceApp.Application.Commands.Base;

namespace FinanceApp.Application.Facades
{
  public class CategoryFacade
  {
    private readonly ICategoryService _categoryService;

    public CategoryFacade(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    public IEnumerable<Category> GetAll()
    {
      return _categoryService.GetAll();
    }

    public IEnumerable<Category> GetByType(OperationType type)
    {
      return _categoryService.GetByType(type);
    }

    public Category GetById(Guid id)
    {
      return _categoryService.GetById(id);
    }

    public void CreateCategory(OperationType type, string name, bool measureTime = false)
    {
      ICommand command = new CreateCategoryCommand(_categoryService, type, name);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Create Category");

      command.Execute();
    }

    public void UpdateCategory(Guid id, OperationType type, string name, bool measureTime = false)
    {
      ICommand command = new EditCategoryCommand(_categoryService, id, type, name);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Update Category");

      command.Execute();
    }

    public void DeleteCategory(Guid id, bool measureTime = false)
    {
      ICommand command = new DeleteCategoryCommand(_categoryService, id);

      if (measureTime)
        command = new TimeMeasurementDecorator(command, "Delete Category");

      command.Execute();
    }
  }
}
