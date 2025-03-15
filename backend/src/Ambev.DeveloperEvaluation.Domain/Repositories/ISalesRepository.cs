using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISalesRepository{
    Task<Sales> CreateAsync(Sales sales, CancellationToken cancellationToken = default);
    Task<Sales> UpdateAsync(Sales sales, CancellationToken cancellationToken = default);
    Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
