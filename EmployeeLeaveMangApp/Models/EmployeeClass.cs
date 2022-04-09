using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveMangApp.Models
{
    public class EmployeeClass
    {
        [Key]
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string EmpGender { get; set; }

        public string LeaveStatus { get; set; }

        public int LeaveId { get; set; }

        public string LeaveType { get; set; }
    }
}
