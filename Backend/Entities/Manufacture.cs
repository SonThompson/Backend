using System.ComponentModel.DataAnnotations;

namespace WebDb.Entities
{
    public class Manufacture
    {
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "Укажите имя производителя товара ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public List<Product>? Products { get; set; } = new();
    }
}
