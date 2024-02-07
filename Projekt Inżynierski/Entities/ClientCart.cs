namespace Projekt_Inżynierski.Entities
{
    public class ClientCart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public ClientCart()
        {
        
        }
        public ClientCart(string id)
        {
            Id = id;
        }

    }
}