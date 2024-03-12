using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Inżynierski.Dtos
{
    public class UpdateProductDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Precision(18, 2), Range(0.01, 9999999.99)]
        public decimal Cost { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}