using System.ComponentModel.DataAnnotations;
using Backend.DAL.Entities;

namespace Backend.DAL.RequestModels.EntityModels
{
    public class ManufactureModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Имя мануфактуры")]
        [Required(ErrorMessage = "Укажите имя производителя товара ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public List<Product>? Products { get; set; } = new();
    }
}
