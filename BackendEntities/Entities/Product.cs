
namespace BackendEntities.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Manufacture? Manufacture { get; set; }
        public Guid? ManufactureId { get; set; }
        public Category? Category { get; set; }
        public Guid? CategoryId { get; set; } 

        public List<Delivery>? Deliveries { get; set; } = new();
        public List<PurchaseItem>? PurchaseItems { get; set; } = new();
        public List<PriceChange>? PriceChanges { get; set; } = new();
    }
}
