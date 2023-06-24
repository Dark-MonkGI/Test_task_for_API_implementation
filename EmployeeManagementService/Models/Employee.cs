namespace EmployeeManagementService.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public int? DepartmentId { get; set; } // Foreign key для Department может быть null


        public Department Department { get; set; } // Навигационное свойство может быть null
        public ICollection<EmployeeTask> EmployeeTasks { get; set; } // Навигационное свойство может быть null
    }
}
