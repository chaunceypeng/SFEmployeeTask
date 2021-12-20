using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFEmployeeTask.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API to create composite primary key for table EmployeeTask
            modelBuilder.Entity<TaskBelongsToEmployee>().HasKey(employee_task => new { employee_task.EmployeeId, employee_task.EmployeeTaskId });
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        public DbSet<TaskBelongsToEmployee> TasksBelongToEmployees { get; set; }
    }
}
