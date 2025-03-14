using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;
    
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }
    
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
         return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
      var procuct =  await _context.Products.FindAsync(id, cancellationToken);
        if (procuct == null)
            return false;
        _context.Products.Remove(procuct);
         await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Product> UpdateByIdAsync(Product product, CancellationToken cancellationToken)
    {
        var p =  await _context.Products.FirstOrDefaultAsync(x=> x.Id == product.Id, cancellationToken);
        if (p == null)
            throw new KeyNotFoundException($"Product with ID {product.Id} not found");
        p.Name = product.Name;
        p.Price = product.Price;
        p.Description = product.Description;
        await _context.SaveChangesAsync(cancellationToken);
        return p;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(id, cancellationToken);
    }
}
