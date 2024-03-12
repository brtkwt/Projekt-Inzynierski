namespace Projekt_Inżynierski.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public int ProductNumber { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Company { get; set; }
    }
}