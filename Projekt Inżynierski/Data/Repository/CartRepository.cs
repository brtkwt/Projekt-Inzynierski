using System.Text.Json;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;
using StackExchange.Redis;

namespace Projekt_Inżynierski.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _dataBase;

        public CartRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _dataBase = connectionMultiplexer.GetDatabase();
        }

        public async Task<ClientCart> GetCartAsync(string cartId)
        {
            var getCart = await _dataBase.StringGetAsync(cartId);

            if(getCart.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<ClientCart>(getCart);
        }

        public async Task<ClientCart> UpdateCartAsync(ClientCart cart)
        {
            var update = await _dataBase.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(10));

            if(update == false)
            {
                return null;
            }

            var getCart = await _dataBase.StringGetAsync(cart.Id);

            return JsonSerializer.Deserialize<ClientCart>(getCart);
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _dataBase.KeyDeleteAsync(cartId);
        }
    }
}