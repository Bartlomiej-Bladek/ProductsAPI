namespace ProductsAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
    }
}
