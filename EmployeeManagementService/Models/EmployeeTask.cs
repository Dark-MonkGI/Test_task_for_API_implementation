namespace EmployeeManagementService.Models
{
    public class EmployeeTask
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public bool IsTaskCompleted { get; set; }
    }
}
