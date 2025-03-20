using Microsoft.Extensions.DependencyInjection;
using FinanceApp.DependencyInjection;
using FinanceApp.Presentation.ConsoleUI;
using FinanceApp.Presentation.ConsoleUI;

class Program
{
  static void Main(string[] args)
  {
    var services = new ServiceCollection()
        .AddFinanceAppServices()
        .BuildServiceProvider();
    var userInterface = services.GetRequiredService<UserInterface>();
    userInterface.Start();
  }
}