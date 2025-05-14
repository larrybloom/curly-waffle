using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private string GetUserId()
        {
            //return User.FindFirstValue(ClaimTypes.NameIdentifier);
            return "249a623f-9e09-4bc8-8432-804d34c0e6f3";
        }


        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var cart = await _cartService.GetCartsAsync(GetUserId());
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto)
        {
            await _cartService.AddItemToCartAsync(GetUserId(), addToCartDto);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto updateCartItemDto)
        {
            await _cartService.UpdateCartItemAsync(GetUserId(), updateCartItemDto);
            return Ok();
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            await _cartService.RemoveCartItemAsync(cartItemId);
            return Ok();
        }
    }
}
