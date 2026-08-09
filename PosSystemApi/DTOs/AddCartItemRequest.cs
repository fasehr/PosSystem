namespace PosSystemApi.DTOs
{
    public class AddCartItemRequest
    {
        public string Sku { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}