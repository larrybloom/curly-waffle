using ECommerce.Application.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IApiService<ProductDto> _apiService;
        private const string Endpoint = "api/products";
        public ProductService(IProductRepository productRepository, IApiService<ProductDto> apiService)
        {
            _productRepository = productRepository;
            _apiService = apiService;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var products =await _productRepository.GetProductByIdAsync(Id);
            return products == null ? null : MapToDto(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int CategoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(CategoryId);
            return products.Select(MapToDto);
        }

        public static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };
        }
    }
}
