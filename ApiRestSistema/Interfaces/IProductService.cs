using ApiRestSistema.Models;

namespace ApiRestSistema.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<Product> DeleteProductAsync(int productId);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int productId);

    }
}
