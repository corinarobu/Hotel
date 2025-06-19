using Hotel.Restaurant.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Interfaces
{
    public interface IProductService
    {
        Task<ProductGetDTO> AddProductAsync(ProductAddDTO productDto);
        Task<ProductGetDTO> UpdateProductAsync(int id_Product, ProductAddDTO productDto);
        Task<bool> DeleteProductAsync(int id_Product);
        Task<ProductGetDTO> GetProductByIdAsync(int id_Product);
        Task<IEnumerable<ProductGetDTO>> GetAllProductAsync();
    }
}
