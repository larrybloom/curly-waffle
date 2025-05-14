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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IApiService<CartDto> _apiService;
        private const string Endpoint = "api/carts";
        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IApiService<CartDto> apiService)
        {
            _apiService = apiService;
            _cartRepository = cartRepository;
            _productRepository = productRepository;

        }
        public async Task AddItemToCartAsync(string userId, AddToCartDto addToCartDto)
        {
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            var existingItem = cartItems.FirstOrDefault(i => i.ProductId == addToCartDto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity = addToCartDto.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity
                };
                await _cartRepository.AddCartItemAsync(cartItem);
            }
        }

        public async Task<CartDto> GetCartsAsync(string userId)
        {
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            var cartItemsDtos = new List<CartItemDto>();

            foreach(var item in cartItems)
            {
                var product = item.Product;
                if(product == null && item.ProductId > 0)
                {
                    product = await _productRepository.GetProductByIdAsync(item.ProductId);
                }

                if(product != null)
                {
                    cartItemsDtos.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        ProductName = product.Name,
                        Quantity = item.Quantity,
                        ProductPrice = product.Price
                    });
                }
            }
            return new CartDto
            {
                CartItems = cartItemsDtos
            };
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task UpdateCartItemAsync(string userId, UpdateCartItemDto updateCartItemDto)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(updateCartItemDto.CartItemId);

            if (cartItem != null && cartItem.UserId == userId)
            {
                cartItem.Quantity = updateCartItemDto.Quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
        }
    }
}
