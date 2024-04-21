using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities.Order;
using Projekt_Inżynierski.Helpers;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryObject query)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(query);
            var oderDtos = _mapper.Map<IReadOnlyList<OrderReturnDto>>(orders);

            return Ok(oderDtos);
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpGet("client")]
        public async Task<IActionResult> GetClientOrders()
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var clientOrders = await _orderRepository.GetClientOrdersAsync(emailAddress);
            var clientOrdersToDto = _mapper.Map<IReadOnlyList<OrderReturnDto>>(clientOrders);

            return Ok(clientOrdersToDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            var orderDto = _mapper.Map<OrderReturnDto>(order);

            if (order == null)
                return NotFound();

            return Ok(orderDto);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("{id:int}/client")]
        public async Task<IActionResult> GetOrderByIdClient(int id)
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var clientOrder = await _orderRepository.GetOrderByIdClientAsync(id, emailAddress);
            var clientOrderToDto = _mapper.Map<OrderReturnDto>(clientOrder);

            if(clientOrder == null)
            {
                return NotFound();
            }
            return Ok(clientOrderToDto);
        }

        [HttpGet("shipping-methods")]
        public async Task<IActionResult> GetShippingMethods()
        {
            var methods = await _orderRepository.GetShippingMethodsAsync();

            return Ok(methods);
        }

        [Authorize(Roles = "Client, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = _mapper.Map<OrderShippingAddress>(createOrderDto.ShippingAddressDto);

            var newOrder = await _orderRepository.CreateOrderAsync(emailAddress, createOrderDto.cartId, createOrderDto.shippingMethodId, shippingAddress);
            var newOrderToDto = _mapper.Map<OrderReturnDto>(newOrder);

            if(newOrder == null)
            {
                return BadRequest("Error occurred while creating new order !");
            }
            return Ok(newOrderToDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchOrder(int id, OrderPatchDto orderDto)
        {
            var patchedOrder = await _orderRepository.PatchOrderAsync(id, orderDto);

            if (patchedOrder == null)
            {
                return NotFound();
            }

            return Ok( _mapper.Map<OrderReturnDto>(patchedOrder));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.DeleteOrderAsync(id);
            
            if(order == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}