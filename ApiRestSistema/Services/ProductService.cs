using ApiRestSistema.Data;
using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ApiRestSistema.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public ProductService(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Product?> AddProductAsync(Product product)
        {
            if (product == null) return null;

            using var context = _dbContextFactory.CreateDbContext();
            var newProduct = context.Vehiculos.Add(product).Entity;
            await context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product?> DeleteProductAsync(int productId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var product = await context.Vehiculos.FindAsync(productId);
            if (product == null) return null;

            context.Vehiculos.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Vehiculos.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Vehiculos.FindAsync(productId);
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            if (product == null) return null;

            using var context = _dbContextFactory.CreateDbContext();
            var existingProduct = await context.Vehiculos.FindAsync(id);
            if (existingProduct == null) return null;

            context.Entry(existingProduct).CurrentValues.SetValues(product);
            await context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
