using FinanceApp.Application.Commands.Base;
using FinanceApp.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.CategoryCommands
{
  public class DeleteCategoryCommand : CommandBase
  {
    private readonly ICategoryService _categoryService;
    private readonly Guid _id;

    public DeleteCategoryCommand(ICategoryService categoryService, Guid id)
    {
      _categoryService = categoryService;
      _id = id;
    }

    public override void Execute()
    {
      _categoryService.Delete(_id);
    }
  }
}
