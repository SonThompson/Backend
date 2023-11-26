using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class ProductModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Имя товара")]
        [Required(ErrorMessage = "Укажите название продукта ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public Manufacture? Manufacture { get; set; }
        public Category? Category { get; set; }

        public List<Delivery>? Deliveries { get; set; } = new();
        public List<PurchaseItem>? PurchaseItems { get; set; } = new();
        public List<PriceChange>? PriceChanges { get; set; } = new();
    }
}
