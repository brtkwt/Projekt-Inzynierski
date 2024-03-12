using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_Inżynierski.Entities
{
    [Index("Name", IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
