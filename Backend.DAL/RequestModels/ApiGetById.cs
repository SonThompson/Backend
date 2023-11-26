using System.ComponentModel.DataAnnotations;

namespace Backend.DAL.RequestModels
{
    public class ApiGetById
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        [Display(Name = "идентификатор")]
        [Required(ErrorMessage = "{0} Обязательный параметр")]
        public Guid Id { get; set; }
    }
}
