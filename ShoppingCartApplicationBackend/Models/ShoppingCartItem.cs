using System.Text.Json.Serialization;

namespace ShoppingCartApplicationBackend.Models
{
    [JsonSerializable(typeof(ShoppingCartItem))]
    [JsonSerializable(typeof(List<ShoppingCartItem>))]
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
