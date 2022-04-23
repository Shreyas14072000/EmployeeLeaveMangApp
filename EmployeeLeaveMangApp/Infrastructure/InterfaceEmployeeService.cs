using EmployeeLeaveMangApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeLeaveMangApp.Infrastructure
{
   
        public interface InterfaceEmployeeService
        {
            Task<IList<EmployeeClass>> GetAllEmployee();

            Task<IList<LeaveDetail>> GetAllLeaveType();

            Task<IList<ApplyPlannedLeave>> GetApplication();


        EmployeeClass GetEmployeeById(int EmpId);

            void InsertEmployee(EmployeeClass employee);
            void UpdateEmployee(EmployeeClass employee);

            void ApplyPL(ApplyPlannedLeave employee);


            void DeletePLeave(int empId);
        
        }
}
