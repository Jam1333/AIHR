using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IInterviewRepository
{
    Task CreateAsync(Interview interview);
    Task<List<Interview>> GetAllAsync(Guid? vacancyId = null);
    Task<Interview?> GetByIdAsync(Guid id);
    Task<bool> ExistsWithIdAsync(Guid id);
    Task UpdateAsync(Interview interview);
    Task DeleteAsync(Guid id);
    Task DeleteByVacancyIdAsync(Guid vacancyId);
}
