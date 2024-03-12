using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Order
{
    public class OrderShippingAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Voivodeship { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ZipCode { get; set; }

        public OrderShippingAddress(string FirstName, string LastName, string Country, string Voivodeship, string City, string Street, string BuildingNumber, string ZipCode)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Country = Country;
            this.Voivodeship = Voivodeship;
            this.City = City;
            this.Street = Street;
            this.BuildingNumber = BuildingNumber;
            this.ZipCode = ZipCode;
        }
    }
}