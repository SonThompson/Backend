using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class PurchaseModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PurshaseDate { get; set; }

        public Store? Store { get; set; }
        public Customer? Customer { get; set; }

        public PurchaseItem? PurchaseItems { get; set; } = new();
    }
}
