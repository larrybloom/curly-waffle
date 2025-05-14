using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface ICartRepository
    {
        Task <IEnumerable<CartItem>> GetCartItemsAsync(string userId);
        Task<CartItem?> GetCartItemAsync(int Id);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(int Id);
    }
}
