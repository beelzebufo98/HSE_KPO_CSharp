using FinanceApp.Application.Facades;
using FinanceApp.Application.Factories;
using FinanceApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FinanceApp.Domain.Interfaces.IFactory;
using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Infrastructure.Repositories;
using System;
using FinanceApp.Presentation.ConsoleUI;


namespace FinanceApp.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddFinanceAppServices(this IServiceCollection services)
    {
      // Регистрация репозиториев
      services.AddSingleton<IBankAccountRepository, InMemoryBankAccountRepository>();
      services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
      services.AddSingleton<IOperationRepository, InMemoryOperationRepository>();

      // Регистрация сервисов
      services.AddSingleton<IBankAccountService, BankAccountService>();
      services.AddSingleton<ICategoryService, CategoryService>();
      services.AddSingleton<IOperationService, OperationService>();
      services.AddSingleton<IAnalyticsService, AnalyticsService>();

      // Регистрация фабрик
      services.AddSingleton<IDomainModelFactory, DomainModelFactory>();

      // Регистрация фасадов
      services.AddSingleton<BankAccountFacade>();
      services.AddSingleton<CategoryFacade>();
      services.AddSingleton<OperationFacade>();
      services.AddSingleton<AnalyticsFacade>();

      // Регистрация UserInterface
      services.AddSingleton<UserInterface>();

      return services;
    }
  }
}
