using System.ComponentModel.DataAnnotations.Schema;

namespace WebDb.Entities
{
    [Table("People")]
    public class Employee
    {
        [Column("EmployeeId")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int? CompanyId { get; set; }      // внешний ключ
        public Company? Company { get; set; } // навигационное свойство Компания

        public List<Pass> Passes { get; set; } = new();
    }

    public class Company
    {
        [Column("Company_id")]
        public int Id { get; set; }
        public string Name { get; set; }

        public Company(string name) => Name = name;

        public List<Employee> Employees { get; set; } = new();
        //public List<Location> Addresses { get; set; } = new();
    }

    //[Owned]
    public class Location
    {
        [Column("Address_id")]
        public int Id { get; set; }
        public string? Address { get; set; }

        public Company? Company { get; set; } // Навигационное свойство компания
        public int CompanyId { get; set; }      // внешний ключ
    }

    //[Owned]
    public class Pass
    {
        public int Id { get; set; }
        public string? PassportSeria { get; set; }
        public string? PassportNumber { get; set; }

        public Employee? Employee { get; set; } // навигационное свойство работника
        public int EmployeeId { get; set; }      // внешний ключ
    }
}
