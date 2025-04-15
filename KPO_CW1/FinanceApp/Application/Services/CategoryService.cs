using FinanceApp.Domain.Interfaces.IFactory;
using FinanceApp.Domain.Interfaces.IRepository;
using FinanceApp.Domain.Interfaces.IServices;
using FinanceApp.Domain.Models.Enums;
using FinanceApp.Domain.Models;
using System;

namespace FinanceApp.Application.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IDomainModelFactory _domainModelFactory;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IOperationRepository operationRepository,
        IDomainModelFactory domainModelFactory)
    {
      _categoryRepository = categoryRepository;
      _operationRepository = operationRepository;
      _domainModelFactory = domainModelFactory;
    }

    public Category GetById(Guid id)
    {
      return _categoryRepository.GetById(id);
    }

    public IEnumerable<Category> GetAll()
    {
      return _categoryRepository.GetAll();
    }

    public IEnumerable<Category> GetByType(OperationType type)
    {
      return _categoryRepository.GetByType(type);
    }

    public Category Create(OperationType type, string name)
    {
      var category = _domainModelFactory.CreateCategory(type, name);
      _categoryRepository.Add(category);
      return category;
    }

    public void Update(Guid id, OperationType type, string name)
    {
      var category = _categoryRepository.GetById(id);
      if (category == null)
        throw new ArgumentException($"Category with ID {id} not found");

      category.UpdateType(type);
      category.UpdateName(name);
      _categoryRepository.Update(category);
    }

    public void Delete(Guid id)
    {
      var category = _categoryRepository.GetById(id);
      if (category == null)
        throw new ArgumentException($"Category with ID {id} not found");
      var operations = _operationRepository.GetByCategory(id);

      foreach (var operation in operations)
      {
        _operationRepository.Delete(operation.Id);
      }
      _categoryRepository.Delete(id);
    }
  }
}
