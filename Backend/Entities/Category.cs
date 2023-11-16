using System.ComponentModel.DataAnnotations;

namespace WebDb.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "Укажите название категории ")]
        [MaxLength(60)]
        public string Name { get; set; }

        public List<Product>? Products { get; set; } = new();
    }
}
