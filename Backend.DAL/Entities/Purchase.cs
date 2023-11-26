
namespace Backend.DAL.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime? PurshaseDate { get; set; }

        public Guid? StoreId { get; set; }
        public Store? Store { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public PurchaseItem? PurchaseItems { get; set; }
        
    }
}
