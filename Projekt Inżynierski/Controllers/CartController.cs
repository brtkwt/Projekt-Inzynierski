using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
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
        public async Task<IActionResult> UpdateCart(ClientCartDto newCartDto)
        {
            var updatedCart = await _cartRepository.UpdateCartAsync( _mapper.Map<ClientCart>(newCartDto) );

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