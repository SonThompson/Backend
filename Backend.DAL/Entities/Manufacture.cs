namespace Backend.DAL.Entities
{
    public class Manufacture
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public List<Product>? Products { get; set; } = new();
    }
}
