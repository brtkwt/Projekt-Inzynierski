namespace Projekt_Inżynierski.Entities.Dto
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } //     ?????

        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
    }
}