using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProductAsync()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetSingleProductAsync(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) return NotFound("Producto no encontrado.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el producto: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddNewProductAsync(Product product)
        {
            if (product == null) return BadRequest("El producto no puede ser nulo.");

            try
            {
                var newProduct = await _productService.AddProductAsync(product);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar el producto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProductAsync(int id)
        {
            try
            {
                var deletedProduct = await _productService.DeleteProductAsync(id);
                if (deletedProduct == null) return NotFound("Producto no encontrado.");

                return Ok(deletedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el producto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProductAsync(int id, Product product)
        {
            if (product == null) return BadRequest("El producto no puede ser nulo.");

            if (id != product.Id)
            {
                return BadRequest("El ID del producto no coincide con el ID en la URL.");
            }

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, product);
                if (updatedProduct == null) return NotFound("Producto no encontrado para actualizar.");

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el producto: {ex.Message}");
            }
        }
    }
}
