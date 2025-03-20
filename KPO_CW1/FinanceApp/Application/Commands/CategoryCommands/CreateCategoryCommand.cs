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
  public class CreateCategoryCommand : CommandBase
  {
    private readonly ICategoryService _categoryService;
    private readonly OperationType _type;
    private readonly string _name;

    public CreateCategoryCommand(ICategoryService categoryService, OperationType type, string name)
    {
      _categoryService = categoryService;
      _type = type;
      _name = name;
    }

    public override void Execute()
    {
      _categoryService.Create(_type, _name);
    }
  }

}
