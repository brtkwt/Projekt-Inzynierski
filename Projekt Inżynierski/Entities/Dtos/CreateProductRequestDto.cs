using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Inżynierski.Entities.Dtos
{
    public class CreateProductRequestDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Precision(16, 2)]
        public decimal Price { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required, MaxLength(100)]
        public string ImagePath { get; set; }   // ???

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}