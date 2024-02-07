using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetCartById(string cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);

            if(cart == null)
            {
                return Ok(new ClientCart(cartId));
            }

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(ClientCart newCart)
        {
            var updatedCart = await _cartRepository.UpdateCartAsync(newCart);

            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string cartId)
        {
            await _cartRepository.DeleteCartAsync(cartId);

            return NoContent();
        }
    }
}