using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Entities.Dtos
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string GivenName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}