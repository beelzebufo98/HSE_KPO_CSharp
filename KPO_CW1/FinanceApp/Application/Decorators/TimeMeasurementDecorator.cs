using FinanceApp.Application.Commands.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Decorators
{
  public class TimeMeasurementDecorator : ICommand
  {
    private readonly ICommand _command;
    private readonly string _commandName;

    public TimeMeasurementDecorator(ICommand command, string commandName)
    {
      _command = command;
      _commandName = commandName;
    }

    public void Execute()
    {
      var stopwatch = Stopwatch.StartNew();

      try
      {
        _command.Execute();
      }
      finally
      {
        stopwatch.Stop();
        Console.WriteLine($"Command '{_commandName}' executed in {stopwatch.ElapsedMilliseconds} ms");
      }
    }
  }
}
