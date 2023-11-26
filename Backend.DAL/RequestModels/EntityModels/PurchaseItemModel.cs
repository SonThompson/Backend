using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class PurchaseItemModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Цена товара")]
        [Required(ErrorMessage = "Укажите цену товара")]
        [MaxLength(60)]
        public double? ProductPrice { get; set; }

        [Display(Name = "Количетсво товара")]
        [Required(ErrorMessage = "Укажите количество товара")]
        [MaxLength(60)]
        public int? ProductCount { get; set; }

        public Product Product { get; set; }
        public Purchase Purchase { get; set; }
    }
}
