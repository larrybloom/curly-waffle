using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
        public int TotalItems => CartItems.Sum(item => item.Quantity);

    }
}
