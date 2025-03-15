using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;
    private readonly IRedisService _redisService;

    public SalesRepository(DefaultContext context
        , IRedisService redisService
        )
    {
        _context = context;
        _redisService = redisService;
    }
  
    public async Task<Sales> CreateAsync(Sales sale, CancellationToken cancellationToken = default)
    {
        await _redisService.SetAsync<Sales>(sale.Id.ToString(), sale,TimeSpan.FromHours(6));
        return sale;
    }

    public async Task<Sales> UpdateAsync(Sales sales, CancellationToken cancellationToken = default)
    {
        await _redisService.UpdateAsync<Sales>(sales.Id.ToString(), sales);
        return sales;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
       return await _redisService.RemoveAsync(id.ToString(), cancellationToken);
    }

    public async Task<Sales> CreateAtDbAsync(Sales saleDb, CancellationToken cancellationToken = default)
    {
        _context.Sales.Add(saleDb);
        await _context.SaveChangesAsync(cancellationToken);
        return saleDb;
    }

    public async Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _redisService.GetAsync<Sales>(id.ToString());
    }
}
