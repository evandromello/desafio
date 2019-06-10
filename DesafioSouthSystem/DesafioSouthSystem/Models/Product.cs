namespace DesafioSouthSystem.Models
{
    public class Product
    {
        public Product(int id, int quantity, decimal price)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
        }

        private int Id { get; set; }
        private int Quantity { get; set; }
        private decimal Price { get; set; }
    }
}