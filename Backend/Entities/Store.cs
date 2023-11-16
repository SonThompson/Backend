using System.ComponentModel.DataAnnotations;

namespace WebDb.Entities
{
    public class Store
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Укажите название филиала ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        //public List<Delivery>? Deliveries { get; set; } = new();
        public List<Purchase>? Purchases { get; set; } = new();
    }
}
