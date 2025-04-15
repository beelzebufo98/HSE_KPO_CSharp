using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.CategoryCommands
{
  public class EditCategoryCommand : CommandBase
  {
    private readonly ICategoryService _categoryService;
    private readonly Guid _id;
    private readonly OperationType _type;
    private readonly string _name;

    public EditCategoryCommand(ICategoryService categoryService, Guid id, OperationType type, string name)
    {
      _categoryService = categoryService;
      _id = id;
      _type = type;
      _name = name;
    }

    public override void Execute()
    {
      _categoryService.Update(_id, _type, _name);
    }
  }
}
