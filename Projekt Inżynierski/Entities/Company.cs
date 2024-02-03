using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Entities
{
    [Index("Name", IsUnique = true)]
    public class Company
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

    }
}
