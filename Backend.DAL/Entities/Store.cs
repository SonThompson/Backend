namespace Backend.DAL.Entities
{
    public class Store
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public List<Delivery>? Deliveries { get; set; } = new();
        public List<Purchase>? Purchases { get; set; } = new();
    }
}
