
namespace Backend.DAL.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public List<Purchase>? Purchases { get; set; } = new();
    }
}
