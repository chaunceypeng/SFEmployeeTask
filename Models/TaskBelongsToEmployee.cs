using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SFEmployeeTask.Models
{
    public class TaskBelongsToEmployee
    {
        
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        
        [ForeignKey("Task")]
        public int EmployeeTaskId { get; set; }

        public virtual EmployeeTask EmployeeTask { get; set; }
    }
}
