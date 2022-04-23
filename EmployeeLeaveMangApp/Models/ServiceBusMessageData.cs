using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeLeaveMangApp.Models
{
    public class ServiceBusMessageData
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string EmpGender { get; set; }

        public string LeaveStatus { get; set; }

        public int LeaveId { get; set; }

        public string LeaveType { get; set; }
        public int LeaveDuration { get; set; }

        public string LeaveReason { get; set; }

        public string Action { get; set; }
    }
}
