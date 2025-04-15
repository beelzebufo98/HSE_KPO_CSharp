using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Commands.Base
{
  public abstract class CommandBase : ICommand
  {
    public abstract void Execute();
  }
}
