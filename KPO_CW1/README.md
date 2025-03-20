# Отчёт по проекту FinanceApp

## Общая идея решения

Проект FinanceApp представляет собой консольное приложение для управления личными финансами. Основной функционал включает:

### Управление банковскими счетами:
- Создание, просмотр, обновление и удаление счетов.
- Автоматическая генерация ID для счетов.

### Управление категориями операций:
- Создание, просмотр, обновление и удаление категорий.
- Категории разделяются на доходы (Income) и расходы (Expense).

### Управление операциями:
- Создание, просмотр, обновление и удаление операций.
- Привязка операций к банковским счетам и категориям.

### Аналитика:
- Расчёт разницы между доходами и расходами за определённый период.
- Группировка операций по категориям.

## Дополнительные особенности реализации:
- Добавлена проверка существования банковского счёта и категории при создании операции.
- Реализовано каскадное удаление операций при удалении банковского счёта.

## Принципы SOLID и GRASP

### Принципы SOLID:

#### Single Responsibility Principle (SRP):
- Каждый класс отвечает за одну задачу. Например:
  - BankAccountService отвечает только за управление банковскими счетами.
  - OperationService отвечает только за управление операциями.
- Классы: BankAccountService, CategoryService, OperationService, AnalyticsService.

#### Open/Closed Principle (OCP):
- Классы открыты для расширения, но закрыты для модификации. Например:
  - Добавление новых типов аналитики не требует изменения существующего кода AnalyticsService.
- Классы: AnalyticsService, DomainModelFactory.

#### Liskov Substitution Principle (LSP):
- Наследники могут использоваться вместо родительских классов. Например:
  - InMemoryBankAccountRepository может быть заменён на другой репозиторий без изменения кода.
- Классы: InMemoryBankAccountRepository, InMemoryCategoryRepository, InMemoryOperationRepository.

#### Interface Segregation Principle (ISP):
- Интерфейсы разделены на мелкие и специфичные. Например:
  - IBankAccountRepository содержит только методы для работы с банковскими счетами.
- Классы: IBankAccountRepository, ICategoryRepository, IOperationRepository.

#### Dependency Inversion Principle (DIP):
- Высокоуровневые модули зависят от абстракций, а не от деталей. Например:
  - BankAccountService зависит от IBankAccountRepository, а не от конкретной реализации.
- Классы: BankAccountService, CategoryService, OperationService.

### Принципы GRASP:

#### Information Expert:
- Ответственность за выполнение задачи возложена на класс, который обладает необходимой информацией. Например:
  - BankAccount содержит информацию о балансе и отвечает за его обновление.
- Классы: BankAccount, Operation.

#### Creator:
- Класс, который создаёт объекты, должен быть ответственным за их инициализацию. Например:
  - DomainModelFactory создаёт объекты доменных моделей.
- Классы: DomainModelFactory.

#### Controller:
- Класс, который управляет потоком выполнения. Например:
  - UserInterface управляет взаимодействием с пользователем.
- Классы: UserInterface.

#### Low Coupling:
- Классы слабо связаны между собой. Например:
  - BankAccountService не зависит от реализации репозитория, только от интерфейса.
- Классы: Все сервисы и фасады.

#### High Cohesion:
- Классы имеют чёткую и узкую ответственность. Например:
  - AnalyticsService отвечает только за аналитику.
- Классы: AnalyticsService, OperationService.

## Паттерны GoF

### 1. Command (Команда):
Используется для инкапсуляции запросов в виде объектов. Это позволяет параметризовать клиентов с различными запросами, ставить запросы в очередь или поддерживать отмену операций.

**Классы:** CreateBankAccountCommand, EditBankAccountCommand, DeleteBankAccountCommand, CreateCategoryCommand, EditCategoryCommand, DeleteCategoryCommand, CreateOperationCommand, EditOperationCommand, DeleteOperationCommand.

**Важность:** Упрощает добавление новых команд и управление ими.

### 2. Decorator (Декоратор):
Используется для добавления новой функциональности объектам без изменения их структуры.

**Классы:** TimeMeasurementDecorator.

**Важность:** Позволяет динамически добавлять функциональность (например, измерение времени выполнения) без изменения основного кода.

### 3. Factory Method (Фабричный метод):
Используется для создания объектов без указания точного класса объекта.

**Классы:** DomainModelFactory.

**Важность:** Упрощает создание объектов и делает код более гибким.

### 4. Facade (Фасад):
Предоставляет простой интерфейс для сложной системы.

**Классы:** BankAccountFacade, CategoryFacade, OperationFacade, AnalyticsFacade.

**Важность:** Упрощает взаимодействие с системой, скрывая сложность.

### 5. Singleton (Одиночка):
Гарантирует, что у класса есть только один экземпляр, и предоставляет глобальную точку доступа к нему.

**Классы:** Репозитории (InMemoryBankAccountRepository, InMemoryCategoryRepository, InMemoryOperationRepository).

**Важность:** Обеспечивает единый источник данных для всего приложения.

## Структура проекта

```
FinanceApp/
│
├── Domain/
│   ├── Models/
│   │   ├── BankAccount.cs
│   │   ├── Category.cs
│   │   ├── Operation.cs
│   │   └── Enums/
│   │       └── OperationType.cs
│   ├── Interfaces/
│   │   ├── IRepository/
│   │   │   ├── IBankAccountRepository.cs
│   │   │   ├── ICategoryRepository.cs
│   │   │   └── IOperationRepository.cs
│   │   ├── IServices/
│   │   │   ├── IBankAccountService.cs
│   │   │   ├── ICategoryService.cs
│   │   │   ├── IOperationService.cs
│   │   │   └── IAnalyticsService.cs
│   │   └── IFactory/
│   │       └── IDomainModelFactory.cs
│   └── Validation/
│       └── DomainModelValidator.cs
│
├── Application/
│   ├── Commands/
│   │   ├── Base/
│   │   │   ├── ICommand.cs
│   │   │   └── CommandBase.cs
│   │   ├── BankAccountCommands/
│   │   │   ├── CreateBankAccountCommand.cs
│   │   │   ├── EditBankAccountCommand.cs
│   │   │   └── DeleteBankAccountCommand.cs
│   │   ├── CategoryCommands/
│   │   │   ├── CreateCategoryCommand.cs
│   │   │   ├── EditCategoryCommand.cs
│   │   │   └── DeleteCategoryCommand.cs
│   │   └── OperationCommands/
│   │       ├── CreateOperationCommand.cs
│   │       ├── EditOperationCommand.cs
│   │       └── DeleteOperationCommand.cs
│   ├── Decorators/
│   │   └── TimeMeasurementDecorator.cs
│   ├── Facades/
│   │   ├── BankAccountFacade.cs
│   │   ├── CategoryFacade.cs
│   │   ├── OperationFacade.cs
│   │   └── AnalyticsFacade.cs
│   ├── Factories/
│   │   └── DomainModelFactory.cs
│   └── Services/
│       ├── BankAccountService.cs
│       ├── CategoryService.cs
│       ├── OperationService.cs
│       └── AnalyticsService.cs
│
├── Infrastructure/
│   └── Repositories/
│       ├── InMemoryBankAccountRepository.cs
│       ├── InMemoryCategoryRepository.cs
│       └── InMemoryOperationRepository.cs
│
├── Presentation/
│   ├── ConsoleUI/
│   │   ├── Menu.cs
│   │   └── UserInterface.cs
│   └── Program.cs
│
└── DependencyInjection/
    └── ServiceCollectionExtensions.cs
```
## Запуск проекта
1. Склонируйте папку с проектом из репозитория на вашу локальную машину
2. Перейдите в папку с проектом, откройте консоль из этой директории и выполните
``` 
cd FinanceApp
```
3. Соберите проект с помощью 
```
dotnet build
```
4.Выполните команду 
``` 
dotnet run --project FinanceApp.csproj
```
