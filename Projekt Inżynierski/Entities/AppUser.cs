using Microsoft.AspNetCore.Identity;

namespace Projekt_Inżynierski.Entities
{
    public class AppUser : IdentityUser
    {
        public string GivenName { get; set; }
        public ShippingAdress shippingAdress { get; set; }
    }
}