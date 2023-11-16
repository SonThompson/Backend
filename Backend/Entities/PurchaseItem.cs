using Microsoft.EntityFrameworkCore;

namespace WebDb.Entities
{
    public class PurchaseItem
    {
        public Guid Id { get; set; }
        public double? ProductPrice { get; set; }
        public int? ProductCount { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
}
