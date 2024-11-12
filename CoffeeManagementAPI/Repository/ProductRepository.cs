using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Prod;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateNewProduct(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p=> p.ProductID == id);
            if(prod == null)
            {
                return false;
            }

            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var prodList = await _context.Products.Include(p=> p.Category).Select(p=> p.toProdDTO()).ToListAsync();

            return prodList;
        }

        public async Task<List<ProductDTO>> GetProductByCategory(int categoryId)
        {
            var prodlist = await _context.Products.Include(p=> p.Category).Where(p=> p.CategoryId == categoryId).Select(s=> s.toProdDTO()).ToListAsync();

            return prodlist;
        }

        public async Task<ProductDTO?> GetProductById(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p=> p.ProductID == id);

            if(prod == null)
            {
                return null;
            }
            return prod.toProdDTO();
        }

        public async Task<List<ProductDTO>> GetProductPagination(PaginationObject pagination)
        {
            var prodSelectable = _context.Products.Select(p => p.toProdDTO()).AsQueryable();

            var prodList = await prodSelectable.Skip(pagination.page * pagination.pageSize).Take(pagination.pageSize).ToListAsync();

            return prodList;
        }

        public async Task<(bool,Product?)> UpdateProduct(Product newProduct, int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p=> p.ProductID == id);

            if(prod == null)
            {
                return (false, null);
            }

            prod.Price = newProduct.Price;
            prod.Image = newProduct.Image;
            prod.ProductName = newProduct.ProductName;
            prod.CategoryId = newProduct.CategoryId;
            prod.IsSoldOut = newProduct.IsSoldOut;
            await _context.SaveChangesAsync();
            return (true,prod);

        }
    }
}
