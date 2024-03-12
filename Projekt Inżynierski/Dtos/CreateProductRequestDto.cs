using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Inżynierski.Dtos
{
    public class CreateProductRequestDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Precision(18, 2), Range(0.01, 9999999.99)]
        public decimal Cost { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}