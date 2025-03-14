using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;

    public SalesRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Sales> CreateAsync(Sales sales, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sales, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sales;
       
    }
    
    public async Task<bool> UpdateAsync(Sales sales, CancellationToken cancellationToken = default)
    {
        var ret = false;
        var sale =  await _context.Sales.FirstOrDefaultAsync(x=> x.Id == sales.Id, cancellationToken);
        if (sale == null) return ret;
        sale = sales;
        await _context.SaveChangesAsync(cancellationToken);
        ret = true;

        return ret;
       
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);
    }
}
