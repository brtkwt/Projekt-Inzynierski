using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Dtos
{
    public class ClientCartDto
    {
        [Required]
        public string Id { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}