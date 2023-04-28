namespace ProductsAPI.Models
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
    }
}
