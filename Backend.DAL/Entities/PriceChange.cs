
namespace Backend.DAL.Entities
{
    public class PriceChange
    {
        public Guid Id { get; set; }
        public double? NewPrice { get; set; }
        public DateTime? DataPriceChange { get; set; }

        public Guid? ProductId { get; set; } // внешний ключ
        public Product? Product { get; set; } // навигационное свойство

    }
}
