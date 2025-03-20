using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Presentation.ConsoleUI
{
  public static class Menu
  {
    public static void ShowMainMenu()
    {
      Console.Clear();
      Console.WriteLine("=== Finance Tracker ===");
      Console.WriteLine("1. Manage Bank Accounts");
      Console.WriteLine("2. Manage Categories");
      Console.WriteLine("3. Manage Operations");
      Console.WriteLine("4. View Analytics");
      Console.WriteLine("5. Exit");
      Console.Write("Select option: ");
    }

    public static void ShowBankAccountMenu()
    {
      Console.Clear();
      Console.WriteLine("=== Bank Accounts ===");
      Console.WriteLine("1. Create Account");
      Console.WriteLine("2. View All Accounts");
      Console.WriteLine("3. Update Account");
      Console.WriteLine("4. Delete Account");
      Console.WriteLine("5. Back to Main Menu");
      Console.Write("Select option: ");
    }
    public static void ShowCategoryMenu()
    {
      Console.Clear();
      Console.WriteLine("=== Categories ===");
      Console.WriteLine("1. Create Category");
      Console.WriteLine("2. View All Categories");
      Console.WriteLine("3. Update Category");
      Console.WriteLine("4. Delete Category");
      Console.WriteLine("5. Back to Main Menu");
      Console.Write("Select option: ");
    }

    public static void ShowOperationMenu()
    {
      Console.Clear();
      Console.WriteLine("=== Operations ===");
      Console.WriteLine("1. Create Operation");
      Console.WriteLine("2. View All Operations");
      Console.WriteLine("3. View by Bank Account");
      Console.WriteLine("4. View by Category");
      Console.WriteLine("5. Update Operation");
      Console.WriteLine("6. Delete Operation");
      Console.WriteLine("7. Back to Main Menu");
      Console.Write("Select option: ");
    }

    public static void ShowAnalyticsMenu()
    {
      Console.Clear();
      Console.WriteLine("=== Analytics ===");
      Console.WriteLine("1. Income vs Expense");
      Console.WriteLine("2. Group by Category");
      Console.WriteLine("3. Back to Main Menu");
      Console.Write("Select option: ");
    }
  }
}
