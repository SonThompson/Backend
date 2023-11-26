using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class DeliveryModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Дата доставки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Количество продуктов")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} Некорреткное значение")]
        public int? ProductCount { get; set; }

        public Product Product { get; set; }
        public Store Store { get; set; }
    }
}
