using Microsoft.EntityFrameworkCore;
using P013EStore.Core.Entities;
using P013EStore.Data.Abstract;
using System.Linq.Expressions;

namespace P013EStore.Data.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByIncludeAsync(int id)
        {
            return await _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByIncludeAsync()
        {
            return await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByIncludeAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products.Where(expression).Include(p => p.Brand).Include(p => p.Category).ToListAsync();
        }
    }
}
