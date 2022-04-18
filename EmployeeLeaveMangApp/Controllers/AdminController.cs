using EmployeeLeaveMangApp.Infrastructure;
using EmployeeLeaveMangApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeLeaveMangApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly InterfaceEmployeeService EmployeeService;
        private readonly ILogger<AdminController> _logger;
        private readonly SendServiceBusMessage _sendServiceBusMessage;


        #region "Constructor init"
        public AdminController(InterfaceEmployeeService EmployeeService, ILogger<AdminController>  logger) 
        {
            _logger = logger;
            _logger.LogInformation("Employee Added");
            this.EmployeeService = EmployeeService;

        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }


      
        public ActionResult GetAllEmployee()
        {
            try
            {
                var EmployeeClass = EmployeeService.GetAllEmployee();
                if (EmployeeClass != null)
                {
                    return View(EmployeeClass);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }

            return BadRequest("Not found");
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
        #region "Add Employee"
        [HttpPost]
        public IActionResult AddEmployee(EmployeeClass EmployeeClass)
        {
            try
            {
                EmployeeService.InsertEmployee(EmployeeClass);

                return Ok("Employee Added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");

        }
        #endregion
        public IActionResult UpdateEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(EmployeeClass employee)
        {
            try
            {
                EmployeeService.UpdateEmployee(employee);
                await _sendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    EmpId = employee.EmpId,
                    EmpName = employee.EmpName,
                    EmpGender = employee.EmpGender,
                    LeaveStatus = employee.LeaveStatus

                });

                return Ok("Leave Approved");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");

        }

    }
}
