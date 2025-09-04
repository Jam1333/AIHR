using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IVacancyRepository
{
    Task CreateAsync(Vacancy vacancy);
    Task<List<Vacancy>> GetAllAsync(Guid? userId = null);
    Task<Vacancy?> GetByIdAsync(Guid id);
    Task<bool> ExistsWithIdAsync(Guid id);
    Task UpdateAsync(Vacancy vacancy);
    Task DeleteAsync(Guid id);
}
