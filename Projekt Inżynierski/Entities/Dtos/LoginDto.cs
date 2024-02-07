using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Dtos
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}