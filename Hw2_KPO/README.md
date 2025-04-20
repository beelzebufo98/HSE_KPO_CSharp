## Отчет по выполнению задания «Управление зоопарком»

### 1. Разработка доменной модели (Domain-Driven Design)

**Сущности (Entities):**
- **Animal** (`Domain/Entities/Animal.cs`)
  - Атрибуты: `Id`, `Type` (AnimalType VO), `Name`, `Date` (дата рождения), `Gender`, `FavorFood` (FoodType VO), `isHealthy`, `isHappily`, `enclosureId`.
  - Методы: `Feed()`, `Heal()`, `SetSick()` — инкапсулируют логику кормления и лечения.
- **Enclosure** (`Domain/Entities/Enclosure.cs`)
  - Атрибуты: `Id`, `EncType` (EnclosureType VO), `Size`, `CurrentSize`, `AnimalsIds`.
  - Методы: `IsCompatibleWithAnimal()`, `AddAnimal()`, `RemoveAnimal()`, `Clean()` — обеспечивают правила размещения и очистки вольера.
- **FeedingSchedule** (`Domain/Entities/FeedingSchedule.cs`)
  - Атрибуты: `Id`, `TypeAnimal` (AnimalType VO), `Time`, `FType` (FoodType VO), `IsActive`.
  - Методы: `UpdateFeedingSchedule()`, `ActivateFeedingSchedule()`, `CancelFeedingSchedule()` — управление расписанием кормлений.

**Value Objects:**
- Перечисления в `Domain/ValueObjects`:
  - `AnimalType` (Mammal, Bird, Fish, Reptile, Amphibian)
  - `EnclosureType` (Cage, Aquarium, Pen, Aviary)
  - `FoodType` (Grass, Meat, FishFood, Fruits, Vegetables)

**Доменные события (Domain Events):**
- **AnimalMovedEvent** (`Application/Events/AnimalMovedEvent.cs`) вызывается при перемещении животного.
- **FeedingTimeEvent** (`Application/Events/FeedingTimeEvent.cs`) вызывается при кормлении.

Все доменные объекты и VO находятся в слое Domain, без внешних зависимостей.

---
### 2. Архитектура проекта (Clean Architecture)

```
Presentation
  └ Controllers (Web API)
Application
  ├ DTOs
  ├ Services (бизнес-логика)
  └ Events (Domain Events)
Domain
  ├ Entities
  ├ ValueObjects
  └ Interfaces (репозитории, обработчики событий)
Infrastructure
  ├ Data (InMemoryZooDatabase)
  ├ Repositories (IAnimalRepository → InMemoryAnimalRepository, …)
  └ Services (EventHandlerService, обработчики событий)
```

- **Направление зависимостей:**
  - Domain не зависит от других слоев.
  - Application зависит только от Domain.
  - Infrastructure реализует интерфейсы Domain.
  - Presentation зависит от интерфейсов Application.

- **Абстракции через интерфейсы:**
  - `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`, `IDomainEventHandler<T>` в Domain.
  - Контроллеры и сервисы используют DI для внедрения абстракций.

- **Изоляция бизнес-логики:**
  - Все правила работы животных, вольеров и расписаний находятся в Domain и Application.
  - Presentation слой выполняет только маппинг DTO и вызовы сервисов.

---
### 3. Слой представления (Web API Controllers)

1. **AnimalsController** (`Presentation/Controllers/AnimalsController.cs`)
   - Endpoints: `GET /api/animals`, `GET /api/animals/{id}`, `POST`, `DELETE`, `PUT /health/setill`, `PUT /health/heal`, `POST /transfer`.
2. **EnclosuresController** (`EnclosuresController.cs`)
   - Endpoints: `GET /api/enclosures`, `GET /{id}`, `POST`, `DELETE`, `POST /{id}/clean`, `GET /{id}/animals`.
3. **FeedingSchedulesController** (`FeedingSchedulesController.cs`)
   - Endpoints: `GET /api/feedingschedules/schedules`, `GET /{id}`, `POST`, `DELETE`, `POST /feed/{animalId}`, `GET /feeding`.
4. **StatisticsController** (`StatisticsController.cs`)
   - Endpoint: `GET /api/statistics` — возврат DTO `ZooStatisticsDto`.

Каждый контроллер использует DTO из `Application/DTOs` для входных и выходных данных.

---
### 4. Тестирование API (Swagger)

- **Swagger UI** подключен в `Program.cs` (в Development): `/swagger/index.html`.
- **Проверенные сценарии:**
  1. Создание новых сущностей:
     - `POST /api/animals` → добавление животного
     - `POST /api/enclosures` → добавление вольера
     - `POST /api/feedingschedules` → добавление записи в расписание кормлений
  2. Чтение данных:
     - `GET` всех и по `id` для животных, вольеров, расписаний
  3. Операции:
     - `PUT /api/animals/{id}/transfer` — перемещение
     - `PUT /api/animals/{id}/health/*` — смена статуса здоровья
     - `POST /api/feedingschedules/feed/{animalId}` — кормление по запросу
     - `GET /api/statistics` — получение статистики

Все эндпоинты проверены на корректность ответов и обработку ошибок.

---
### 5. Хранение данных (In-Memory)

- **InMemoryZooDatabase** (`Infrastructure/Data/InMemoryZooDatabase.cs`):
  - Коллекции: `List<Animal>`, `List<Enclosure>`, `List<FeedingSchedule>`.
- **Репозитории**
  - `InMemoryAnimalRepository`, `InMemoryEnclosureRepository`, `InMemoryFeedingScheduleRepository` реализуют соответствующие интерфейсы.
  - Логирование CRUD-операций.

Данные сохраняются только в памяти на время работы приложения.

---
### 6. Итоги

#### a) Реализованные пункты функционала

- **Добавление/удаление**: животные (`AnimalsController` + `InMemoryAnimalRepository`), вольеры (`EnclosuresController` + `InMemoryEnclosureRepository`), расписания кормлений (`FeedingSchedulesController` + `InMemoryFeedingScheduleRepository`).
- **Просмотр**: списков и деталей животных, вольеров, расписаний.
- **Перемещение животного**: `AnimalTransferService`, API `AnimalsController.TransferAnimal`, событие `AnimalMovedEvent`.
- **Кормление**: по расписанию (`FeedingService.ProcessScheduledFeedings` + `FeedingTimeEvent`), вручную (`FeedAnimal`).
- **Статистика зоопарка**: `ZooStatisticsService.GetZooStatistics`, DTO `ZooStatisticsDto`.
- **Уборка вольера**: `EnclosuresController.CleanEnclosure`, метод `Enclosure.Clean()`.
- **Статус здоровья**: `AnimalsController.SetIllAnimal`, `HealAnimal`.

#### b) Примененные концепции DDD и принципы Clean Architecture

| Концепция / Принцип                 | Где применено (класс/модуль)                                       |
|--------------------------------------|-------------------------------------------------------------------|
| Value Object                        | `AnimalType`, `EnclosureType`, `FoodType` (Domain/ValueObjects)    |
| Инкапсуляция бизнес-правил          | Методы `Feed()`, `Heal()`, `SetSick()` в `Animal`                  |
|                                     | `AddAnimal()`, `RemoveAnimal()`, `IsCompatibleWithAnimal()` в `Enclosure` |
|                                     | Управление расписанием в `FeedingSchedule`                         |
| Доменные события                   | `AnimalMovedEvent`, `FeedingTimeEvent` и их хендлеры               |
| Слои зависят внутрь                 | Domain не зависит от Application/Infrastructure                    |
| Абстракции через интерфейсы        | `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`, `IDomainEventHandler<T>` |
| Изоляция бизнес-логики             | `AnimalTransferService`, `FeedingService`, `ZooStatisticsService` (Application/Services) |
| Presentation ↔ Application через DTO| `Application/DTOs` и контроллеры в `Presentation`                  |

