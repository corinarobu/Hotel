using Hotel.Restaurant.Data;
using Hotel.Restaurant.DTOs;
using Hotel.Restaurant.Entities;
using Hotel.Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Restaurant.Services
{
    public class ProductService : IProductService
    {
        private readonly RestaurantDbContext _context;

        public ProductService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<ProductGetDTO> AddProductAsync(ProductAddDTO productDto)
        {
            var product = new Products { Name_Product = productDto.Name_Product, Type_Of_Product = productDto.Type_Of_Product, Description_Product = productDto.Description_Product, Meal_Id = productDto.Meal_Id };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductGetDTO { Id_Product = product.Id_Product, Type_Of_Product = product.Type_Of_Product, Description_Product = product.Description_Product, Price = productDto.Price, Meal_Id = product.Meal_Id };
        }

        public async Task<bool> DeleteProductAsync(int id_Product)
        {
            var product = await _context.Products.FindAsync(id_Product);
            if (product == null) { return false; }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ProductGetDTO> UpdateProductAsync(int id_Product, ProductAddDTO productDto)
        {
            var product = await _context.Products.FindAsync(id_Product);
            if (product == null)
            {
                return null;
            }

            product.Name_Product = productDto.Name_Product;
            product.Type_Of_Product = productDto.Type_Of_Product;
            product.Description_Product = productDto.Description_Product;
            product.Price = productDto.Price;
            product.Meal_Id=productDto.Meal_Id;


            await _context.SaveChangesAsync();

            return new ProductGetDTO
            {
                Id_Product = product.Id_Product,
                Name_Product = product.Name_Product,
                Description_Product = product.Description_Product,
                Type_Of_Product=product.Type_Of_Product,
                Price = product.Price,
                Meal_Id = product.Meal_Id
            };
        }
        public async Task<ProductGetDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductGetDTO
            {
                Id_Product = product.Id_Product,
                Name_Product = product.Name_Product,
                Description_Product = product.Description_Product,
                Type_Of_Product = product.Type_Of_Product,
                Price = product.Price,
                Meal_Id = product.Meal_Id
            };
        }
        public async Task<IEnumerable< ProductGetDTO>> GetAllProductAsync()
        {
            var product = await _context.Products.ToListAsync();

            return product.Select(product => new ProductGetDTO
            {

                Id_Product = product.Id_Product,
                Name_Product = product.Name_Product,
                Description_Product = product.Description_Product,
                Type_Of_Product = product.Type_Of_Product,
                Price = product.Price,
                Meal_Id = product.Meal_Id
            });
        }

    }
}
