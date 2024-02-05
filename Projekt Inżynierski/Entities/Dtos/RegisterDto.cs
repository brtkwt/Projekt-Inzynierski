using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Entities.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}