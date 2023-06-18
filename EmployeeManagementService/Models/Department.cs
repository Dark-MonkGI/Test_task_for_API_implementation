namespace EmployeeManagementService.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletionDate { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
