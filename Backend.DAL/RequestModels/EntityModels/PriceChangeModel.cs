using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class PriceChangeModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Цена товара")]
        [Required(ErrorMessage = "Укажите цену")]
        [MaxLength(60)]
        public double? NewPrice { get; set; }

        [Display(Name = "Дата изменения цены товара")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataPriceChange { get; set; }

        public Product? Product { get; set; }
    }
}
