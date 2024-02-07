using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Interfaces
{
    public interface ICartRepository
    {
        Task<ClientCart> GetCartAsync(string cartId);
        Task<ClientCart> UpdateCartAsync(ClientCart cart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}