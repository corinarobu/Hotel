
using Hotel.Restaurant.DTOs;
using Hotel.Restaurant.Interfaces;
using Hotel.Restaurant.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Controllers.Restaurant
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

            [HttpPost]
            public async Task<IActionResult> AddProduct(ProductAddDTO productAddDTO)
            {
                var result = await _productService.AddProductAsync(productAddDTO);
                return CreatedAtAction(nameof(GetProductById), new { id = result.Id_Product }, result);
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetProductById(int id)
            {
                var result = await _productService.GetProductByIdAsync(id);
                if (result == null) return NotFound();

                return Ok(result);
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(int id)
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result == null) return NotFound();
                return NoContent();
            }
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductAddDTO productAddDTO)
            {
                if (productAddDTO == null)
                {
                    return BadRequest("Product data is required.");
                }

                var updatedProduct = await _productService.UpdateProductAsync(id, productAddDTO);
                if (updatedProduct == null)
                {
                    return NotFound($"Product with ID {id} was not found.");
                }

                return Ok(updatedProduct);
            }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductGetDTO>>> GetAllProducts()
        {
            var product = await _productService.GetAllProductAsync();
            return Ok(product);
        }
    }
    }
