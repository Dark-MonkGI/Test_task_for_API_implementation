using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>()
                .HasKey(et => new { et.EmployeeId, et.TaskId });

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(et => et.Employee)
                .WithMany(e => e.EmployeeTasks)
                .HasForeignKey(et => et.EmployeeId);

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(et => et.Task)
                .WithMany(t => t.EmployeeTasks)
                .HasForeignKey(et => et.TaskId);
        }

    }
}
