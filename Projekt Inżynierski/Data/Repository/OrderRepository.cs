using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities.Order;
using Projekt_Inżynierski.Helpers;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(DataContext context, IProductRepository productRepository, ICartRepository cartRepository )
        {
            _context = context;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }


        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(OrderQueryObject query)
        {
            var orders = _context.Orders.Include(o => o.OrderItems).Include(o => o.ShippingMethod).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.EmailSearch))
            {
                orders = orders.Where(o => o.ClientEmail == query.EmailSearch);
            }

            switch (query.SortBy)
            {
                case "dateasc":
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
                case "datedesc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.Id);
                    break;
            }

            var skipNumber = query.PageSize * (query.PageNumber - 1);

            return await orders.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<IReadOnlyList<Order>> GetClientOrdersAsync(string clientEmail)
        {
            return await _context.Orders.Include(o => o.OrderItems).Include(o => o.ShippingMethod).OrderBy(o => o.OrderDate).Where( c => c.ClientEmail == clientEmail).ToListAsync(); 
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderItems).Include(o => o.ShippingMethod).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> GetOrderByIdClientAsync(int id, string clientEmail)
        {
            return await _context.Orders.Include(o => o.OrderItems).Include(o => o.ShippingMethod).FirstOrDefaultAsync(x => x.Id == id && x.ClientEmail == clientEmail);
        }

        public async Task<Order> CreateOrderAsync(string clientEmail, string cartId, int deliveryMethodId, OrderShippingAddress orderShippingAddress)
        {
            // get cart from the repo
            var cart = await _cartRepository.GetCartAsync(cartId);

            // get items from the product repo
            var cartItems = new List<OrderItem>();

            foreach(var item in cart.Items)
            {
                var productDB = await _productRepository.GetProductByIdAsync( item.Id );
                var productOrdered = new ProductOrdered(productDB.Id, productDB.Name, productDB.ImagePath);
                var orderItem = new OrderItem(productOrdered, productDB.Cost, item.ProductNumber);

                cartItems.Add(orderItem);
            }

            // get delivery method from repo
            var shippingMethod = await _context.ShippingMethods.FirstOrDefaultAsync( s => s.Id == deliveryMethodId);
            
            // calculate sumcost
            decimal sumCost = 0;
            foreach(var cartItem in cartItems )
            {
                sumCost += cartItem.ProductNumber * cartItem.Cost;
            }

            // create order
            var newOrder = new Order(clientEmail, cartItems, sumCost, orderShippingAddress, shippingMethod);

            // save to database
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            // return order 
            return newOrder;
        }

        public async Task<Order> PatchOrderAsync(int id, OrderPatchDto orderDto)
        {
            var existingOrder = await _context.Orders.Include(o => o.OrderItems).Include(o => o.ShippingMethod).FirstOrDefaultAsync(x => x.Id == id);

            if(existingOrder == null)
            {
                return null;
            }

            if(!orderDto.PatchClientEmail.IsNullOrEmpty())
            {
                existingOrder.ClientEmail = orderDto.PatchClientEmail;
            }

            if( orderDto.PatchShippingMethodId != null)
            {
                var patchedShippingMethod = await _context.ShippingMethods.FirstOrDefaultAsync( s => s.Id == orderDto.PatchShippingMethodId);
                existingOrder.ShippingMethod = patchedShippingMethod;
            }

            if( orderDto.PatchOrderShippingAddress != null)
            {
                existingOrder.OrderShippingAddress.FirstName = orderDto.PatchOrderShippingAddress.FirstName;
                existingOrder.OrderShippingAddress.LastName = orderDto.PatchOrderShippingAddress.LastName;
                existingOrder.OrderShippingAddress.Country = orderDto.PatchOrderShippingAddress.Country;
                existingOrder.OrderShippingAddress.Voivodeship = orderDto.PatchOrderShippingAddress.Voivodeship;
                existingOrder.OrderShippingAddress.City = orderDto.PatchOrderShippingAddress.City;
                existingOrder.OrderShippingAddress.Street = orderDto.PatchOrderShippingAddress.Street;
                existingOrder.OrderShippingAddress.BuildingNumber = orderDto.PatchOrderShippingAddress.BuildingNumber;
                existingOrder.OrderShippingAddress.ZipCode = orderDto.PatchOrderShippingAddress.ZipCode;
            }

            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<Order> DeleteOrderAsync(int id)
        {
            var existingOrder = await _context.Orders.FindAsync(id);

            if(existingOrder == null)
                return null;

            _context.Orders.Remove(existingOrder);
            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<IReadOnlyList<ShippingMethod>> GetShippingMethodsAsync()
        {
            return await _context.ShippingMethods.ToListAsync();
        }
    }
}