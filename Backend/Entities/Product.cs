using System.ComponentModel.DataAnnotations;

namespace WebDb.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Укажите название продукта ")]
        [MaxLength(60)]
        public string? Name { get; set; }
        public Manufacture? Manufacture { get; set; } // навигационное свойство
        public Guid? ManufactureId { get; set; } // внешний ключ
        public Category? Category { get; set; } // навигационное свойство
        public Guid? CategoryId { get; set; } // внешний ключ

        //public List<Delivery>? Deliveries { get; set; } = new();
        //public List<PurchaseItem>? PurchaseItems { get; set; } = new();
        //public List<PriceChange>? PriceChanges { get; set; } = new();
    }
}
