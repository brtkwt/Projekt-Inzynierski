using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Inżynierski.Dtos
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required, Precision(18, 2), Range(0.01, 9999999.99)]
        public decimal Cost { get; set; }
        [Required, Range(1, 9999999)]
        public int ProductNumber { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Company { get; set; }
    }
}