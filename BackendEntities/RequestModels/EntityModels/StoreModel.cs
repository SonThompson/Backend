﻿using System.ComponentModel.DataAnnotations;
using BackendEntities.Entities;

namespace BackendEntities.RequestModels.EntityModels
{
    public class StoreModel
    {
        [Display(Name = "Идентификатор")]
        public Guid Id { get; set; }

        [Display(Name = "Имя филиала")]
        [Required(ErrorMessage = "Укажите название филиала ")]
        [MaxLength(60)]
        public string? Name { get; set; }

        public List<Delivery>? Deliveries { get; set; } = new();
        public List<Purchase>? Purchases { get; set; } = new();
    }
}
