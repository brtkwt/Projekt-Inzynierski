namespace Projekt_Inżynierski.Dtos
{
    public class ProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImageAdress { get; set; }

        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
    }
}