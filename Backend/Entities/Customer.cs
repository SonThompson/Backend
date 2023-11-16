using System.ComponentModel.DataAnnotations;

namespace WebDb.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Укажите имя пользователя ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public List<Purchase>? Purchases { get; set; } = new();
    }
}
