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
        public AdminController(InterfaceEmployeeService employeeService, ILogger<AdminController>  logger , SendServiceBusMessage sendServiceBusMessage) 
        {
            _logger = logger;
            _logger.LogInformation("Employee Added");
            EmployeeService = employeeService;
            _sendServiceBusMessage = sendServiceBusMessage;

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
        public async Task<IActionResult> GetAllEmployee()
        {
            _logger.LogInformation("student endpoint starts");
            var employee = await EmployeeService.GetAllEmployee();
            try
            {
                if (employee == null) return NotFound();
                _logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
            
            return View(employee);
        }
        public IActionResult AddEmployee() 
        {
            return View();
        }
       
        #region "Add Employee"
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeClass employeeClass)
        {
            try
            {
                EmployeeService.InsertEmployee(employeeClass);
                
                await _sendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    EmpId = employeeClass.EmpId,
                    EmpName = employeeClass.EmpName,
                    EmpGender = employeeClass.EmpGender,
                    Action = "Added"
                });

            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            ViewBag.Message = string.Format("Employee Added Successfully");
            return View();

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
                    LeaveStatus = employee.LeaveStatus,
                    Action = "Approved for Leave"

                });

               
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            ViewBag.Message = string.Format("Leave Approved Successfully");
            return View();

        }

    }
}
