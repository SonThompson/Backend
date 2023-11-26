
using System.ComponentModel.DataAnnotations;

namespace Backend.DAL.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? ProductCount { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

    }
}
