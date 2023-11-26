using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class CustomerModel
    {
        [Display(Name = "идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Имя, фамилия заказчика")]
        [Required(ErrorMessage = "Укажите имя пользователя ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public List<Purchase>? Purchases { get; set; } = new();
    }
}
