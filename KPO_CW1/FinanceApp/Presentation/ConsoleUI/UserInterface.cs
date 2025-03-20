using FinanceApp.Application.Facades;
using FinanceApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Presentation.ConsoleUI
{
  public class UserInterface
  {
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;
    private readonly AnalyticsFacade _analyticsFacade;

    public UserInterface(
        BankAccountFacade bankAccountFacade,
        CategoryFacade categoryFacade,
        OperationFacade operationFacade,
        AnalyticsFacade analyticsFacade)
    {
      _bankAccountFacade = bankAccountFacade;
      _categoryFacade = categoryFacade;
      _operationFacade = operationFacade;
      _analyticsFacade = analyticsFacade;
    }
    public void Start()
    {
      while (true)
      {
        try
        {
          Menu.ShowMainMenu();
          var input = Console.ReadLine();

          switch (input)
          {
            case "1": HandleBankAccounts(); break;
            case "2": HandleCategories(); break;
            case "3": HandleOperations(); break;
            case "4": HandleAnalytics(); break;
            case "5": return;
            default: ShowError("Invalid option!"); break;
          }
        }
        catch (Exception ex)
        {
          ShowError(ex.Message);
        }
      }
    }
    private void HandleBankAccounts()
    {
      while (true)
      {
        Menu.ShowBankAccountMenu();
        var input = Console.ReadLine();

        switch (input)
        {
          case "1": CreateBankAccount(); break;
          case "2": ListBankAccounts(); break;
          case "3": UpdateBankAccount(); break;
          case "4": DeleteBankAccount(); break;
          case "5": return;
          default: ShowError("Invalid option!"); break;
        }
      }
    }
    private void CreateBankAccount()
    {
      Console.Write("Enter account name: ");
      var name = Console.ReadLine();
      var balance = GetDecimalInput("Enter initial balance: ");

      _bankAccountFacade.CreateBankAccount(name, balance);
      ShowSuccess("Account created!");
    }

    private void ListBankAccounts()
    {

      var accounts = _bankAccountFacade.GetAll();
      PrintTable(accounts,
          new[] { "ID", "Name", "Balance" },
          a => new object[] { a.Id, a.Name, $"{a.Balance}" });

      Console.ReadKey();
    }

    
    private void UpdateBankAccount()
    {
      ListBankAccounts();
      var accounts = _bankAccountFacade.GetAll();
      if (accounts.Count() != 0)
      {
        var id = GetGuidInput("Enter account ID: ");
        Console.Write("Enter new name: ");
        var name = Console.ReadLine();

        _bankAccountFacade.UpdateBankAccount(id, name);
        ShowSuccess("Account updated!");
      }
      else
      {
        ShowError("The list of bank accounts is empty");
      }
    }

    private void DeleteBankAccount()
    {
      ListBankAccounts();
      var accounts = _bankAccountFacade.GetAll();
      if (accounts.Count() != 0)
      {
        var id = GetGuidInput("Enter account ID: ");
        _bankAccountFacade.DeleteBankAccount(id);
        ShowSuccess("Account deleted!");
      }
      else
      {
        ShowError("The list of bank accounts is empty");
      }
    }
    private void HandleCategories()
    {
      while (true)
      {
        Menu.ShowCategoryMenu();
        var input = Console.ReadLine();

        switch (input)
        {
          case "1": CreateCategory(); break;
          case "2": ListCategories(); break;
          case "3": UpdateCategory(); break;
          case "4": DeleteCategory(); break;
          case "5": return;
          default: ShowError("Invalid option!"); break;
        }
      }
    }
    private void CreateCategory()
    {
      Console.Write("Enter category type (1-Income, 2-Expense): ");
      var type = Console.ReadLine() == "1" ? OperationType.Income : OperationType.Expense;

      Console.Write("Enter category name: ");
      var name = Console.ReadLine();

      _categoryFacade.CreateCategory(type, name);
      ShowSuccess("Category created!");
    }

    private void ListCategories()
    {
      var categories = _categoryFacade.GetAll();
      PrintTable(categories,
          new[] { "ID", "Type", "Name" },
          c => new object[] { c.Id, c.Type, c.Name });

      Console.ReadKey();
    }
    private void UpdateCategory()
    {
      ListCategories();
      var categories = _categoryFacade.GetAll();
      if (categories.Count() != 0)
      {
        var id = GetGuidInput("Enter category ID: ");
        Console.Write("Enter new type (1-Income, 2-Expense): ");
        var type = Console.ReadLine() == "1" ? OperationType.Income : OperationType.Expense;
        Console.Write("Enter new name: ");
        var name = Console.ReadLine();

        _categoryFacade.UpdateCategory(id, type, name);
        ShowSuccess("Category updated!");
      }
      else
      {
        ShowError("The list of categories is empty");
      }
    }

    private void DeleteCategory()
    {
      ListCategories();
      var categories = _categoryFacade.GetAll();
      if (categories.Count() != 0)
      {
        var id = GetGuidInput("Enter category ID: ");
        _categoryFacade.DeleteCategory(id);
        ShowSuccess("Category deleted!");
      }
      else
      {
        ShowError("The list of categories is empty");
      }
    }


    private void HandleOperations()
    {
      while (true)
      {
        Menu.ShowOperationMenu();
        var input = Console.ReadLine();

        switch (input)
        {
          case "1": CreateOperation(); break;
          case "2": ListAllOperations(); break;
          case "3": ListOperationsByAccount(); break;
          case "4": ListOperationsByCategory(); break;
          case "5": UpdateOperation(); break;
          case "6": DeleteOperation(); break;
          case "7": return;
          default: ShowError("Invalid option!"); break;
        }
      }
    }
    private void CreateOperation()
    {
      try
      {
        Console.Write("Operation type (1-Income, 2-Expense): ");
        var type = Console.ReadLine() == "1" ? OperationType.Income : OperationType.Expense;

        var accountId = GetGuidInput("Bank Account ID: ");
        var amount = GetDecimalInput("Amount: ");
        var categoryId = GetGuidInput("Category ID: ");
        var date = GetDateInput("Date (yyyy-MM-dd): ");
        Console.Write("Description: ");
        var description = Console.ReadLine();

        _operationFacade.CreateOperation(type, accountId, amount, date, description, categoryId);
        ShowSuccess($"Operation created!");
      }
      catch (Exception ex)
      {
        ShowError(ex.Message);
      }
    }
    private void ListAllOperations()
    {
      var operations = _operationFacade.GetAll();
      PrintTable(operations,
          new[] { "ID", "Type", "Amount", "Date", "Description", "Account ID", "Category ID" },
          o => new object[] { o.Id, o.Type, $"{o.Amount}", o.Date.ToShortDateString(), o.Description, o.BankAccountId, o.CategoryId });

      Console.ReadKey();
    }
    private void ListOperationsByAccount()
    {
      ListBankAccounts();
      var accountId = GetGuidInput("Enter account ID: ");
      var operations = _operationFacade.GetByBankAccount(accountId);
      if (operations.Count() != 0)
      {
        PrintTable(operations,
            new[] { "ID", "Type", "Amount", "Date", "Description", "Category ID" },
            o => new object[] { o.Id, o.Type, $"{o.Amount}", o.Date.ToShortDateString(), o.Description, o.CategoryId });

        Console.ReadKey();
      }
      else
      {
        ShowError("The list of operations for certain account is empty");
        Console.ReadKey();
      }
    }
    private void UpdateOperation()
    {
      ListAllOperations();
      var operations = _operationFacade.GetAll();
      if (operations.Count() != 0)
      {
        var id = GetGuidInput("Enter operation ID to update: ");
        Console.Write("Operation type (1-Income, 2-Expense): ");
        var type = Console.ReadLine() == "1" ? OperationType.Income : OperationType.Expense;

        var amount = GetDecimalInput("Amount: ");
        var date = GetDateInput("Date (yyyy-MM-dd): ");
        Console.Write("Description: ");
        var description = Console.ReadLine();
        var categoryId = GetGuidInput("Category ID: ");

        _operationFacade.UpdateOperation(id, type, amount, date, description, categoryId);
        ShowSuccess("Operation updated!");
      }
      else
      {
        ShowError("The list of operations is empty");
      }
    }

    private void DeleteOperation()
    {
      ListAllOperations();
      var operations = _operationFacade.GetAll();
      if (operations.Count() != 0)
      {
        var id = GetGuidInput("Enter operation ID to delete: ");
        _operationFacade.DeleteOperation(id);
        ShowSuccess("Operation deleted!");
      }
      else
      {
        ShowError("The list of operations is empty");
      }
    }
    private void ListOperationsByCategory()
    {
      ListCategories();
      var categoryId = GetGuidInput("Enter category ID: ");
      var operations = _operationFacade.GetByCategory(categoryId);
      if (operations.Count() != 0)
      {
        PrintTable(operations,
            new[] { "ID", "Type", "Amount", "Date", "Description", "Account ID" },
            o => new object[] { o.Id, o.Type, $"{o.Amount}", o.Date.ToShortDateString(), o.Description, o.BankAccountId });
        Console.ReadKey();
      }
      else
      {
        ShowError("The list of operations for certain category is empty");
        Console.ReadKey();
      }

     
    }
    private void PrintTable<T>(IEnumerable<T> items, string[] headers, Func<T, object[]> rowData)
    {
      Console.WriteLine("\n" + string.Join(" | ", headers));
      Console.WriteLine(new string('-', headers.Sum(h => h.Length + 3)));

      foreach (var item in items)
      {
        var values = rowData(item).Select(x => x.ToString());
        Console.WriteLine(string.Join(" | ", values));
      }
    }
    private void HandleAnalytics()
    {
      while (true)
      {
        Menu.ShowAnalyticsMenu();
        var input = Console.ReadLine();

        switch (input)
        {
          case "1": ShowIncomeExpenseDifference(); break;
          case "2": ShowGroupedByCategory(); break;
          case "3": return;
          default: ShowError("Invalid option!"); break;
        }
      }
    }
    private void ShowIncomeExpenseDifference()
    {
      var (start, end) = GetDateRange();
      var difference = _analyticsFacade.CalculateIncomeExpenseDifference(start, end);

      Console.WriteLine($"\nNet difference (Income - Expense): {difference}");
      Console.ReadKey();
    }

    private void ShowGroupedByCategory()
    {
      var (start, end) = GetDateRange();
      Console.Write("Type (1-Income, 2-Expense): ");
      var type = Console.ReadLine() == "1" ? OperationType.Income : OperationType.Expense;

      var groupedData = _analyticsFacade.GroupOperationsByCategory(start, end, type);

      Console.WriteLine($"\n=== {type} by Category ===");
      foreach (var item in groupedData)
      {
        Console.WriteLine($"{item.Key.Name.PadRight(20)}: {item.Value}");
      }

      Console.ReadKey();
    }

    private (DateTime start, DateTime end) GetDateRange()
    {
      Console.Write("Start date (yyyy-MM-dd): ");
      var start = DateTime.Parse(Console.ReadLine());

      Console.Write("End date (yyyy-MM-dd): ");
      var end = DateTime.Parse(Console.ReadLine());

      return (start, end);
    }
    private DateTime GetDateInput(string prompt)
    {
      DateTime date;
      while (true)
      {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
          return date;
        }
        ShowError("Error: Incorrect date format. Please enter the date in the yyyy-MM-dd format.");
      }
    }
    private decimal GetDecimalInput(string prompt)
    {
      while (true)
      {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out var result))
        {
          return result;
        } 
        ShowError("Invalid decimal value!");
      }
    }

    private Guid GetGuidInput(string prompt)
    {
      while (true)
      {
        Console.Write(prompt);
        if (Guid.TryParse(Console.ReadLine(), out var result))
          return result;

        ShowError("Invalid GUID format!");
      }
    }
    private void ShowError(string message)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"\nError: {message}");
      Console.ResetColor();
      Console.ReadKey();
    }

    private void ShowSuccess(string message)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"\n{message}");
      Console.ResetColor();
      Console.ReadKey();
    }
  }
}
