using ApiRestSistema.Data;
using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestSistema.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<Product?> AddProductAsync(Product product)
        {
            if (product == null) return null;

            var newProduct = _context.Vehiculos.Add(product).Entity;
            await _context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product?> DeleteProductAsync(int productId)
        {
            var product = await _context.Vehiculos.FindAsync(productId);
            if (product == null) return null;

            _context.Vehiculos.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Vehiculos.FindAsync(productId);
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            if (product == null) return null;

            var existingProduct = await _context.Vehiculos.FindAsync(id);
            if (existingProduct == null) return null;

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}