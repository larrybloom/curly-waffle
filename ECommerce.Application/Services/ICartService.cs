using ECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartsAsync(string userId);
        Task AddItemToCartAsync(string userId, AddToCartDto addToCartDto);
        Task UpdateCartItemAsync(string userId, UpdateCartItemDto updateCartItemDto);
        Task RemoveCartItemAsync(int cartItemId);
    }
}
