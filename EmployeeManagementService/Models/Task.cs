namespace EmployeeManagementService.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }

        public ICollection<EmployeeTask> EmployeeTasks { get; set; } // Навигационное свойство
    }
}
