using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // navigation properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
