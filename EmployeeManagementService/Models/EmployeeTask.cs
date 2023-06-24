namespace EmployeeManagementService.Models
{
    public class EmployeeTask
    {
        public int? EmployeeId { get; set; } // Foreign key для Employee
        public Employee Employee { get; set; } // Навигационное свойство
        public int? TaskId { get; set; } // Foreign key для Task
        public Task Task { get; set; } // Навигационное свойство

        public bool IsTaskCompleted { get; set; }
    }
}
