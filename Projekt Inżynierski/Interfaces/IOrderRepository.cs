using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities.Order;
using Projekt_Inżynierski.Helpers;

namespace Projekt_Inżynierski.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderQueryObject query);
        Task<IReadOnlyList<Order>> GetClientOrdersAsync(string clientEmail);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByIdClientAsync(int id, string clientEmail);
        Task<Order> CreateOrderAsync(string clientEmail, string cartId, int deliveryMethod, OrderShippingAddress orderShipping);
        Task<Order> PatchOrderAsync(int id, OrderPatchDto orderDto);
        Task<Order> DeleteOrderAsync(int id);
        Task<IReadOnlyList<ShippingMethod>> GetShippingMethodsAsync();
    }
}