using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IAnalysisRepository
{
    Task CreateAsync(Analysis analysis);
    Task<List<Analysis>> GetAllAsync(Guid? vacancyId = null);
    Task<Analysis?> GetByIdAsync(Guid id);
    Task<bool> ExistsWithIdAsync(Guid id);
    Task UpdateAsync(Analysis analysis);
    Task DeleteAsync(Guid id);
    Task DeleteByVacancyId(Guid vacancyId);
}
