using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SFEmployeeTask.Models
{
    public class EmployeeTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [NotMapped]
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
