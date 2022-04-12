using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using EmployeeLeaveMangApp.Models;
using EmployeeLeaveMangApp.Infrastructure;

namespace EmployeeLeaveMangApp.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly InterfaceEmployeeService EmployeeService;
        private readonly ILogger<EmployeeController> _logger;


        #region "Constructor init"
        public EmployeeController(InterfaceEmployeeService EmployeeService, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _logger.LogInformation("Employee Added");
            this.EmployeeService = EmployeeService;
        }
        #endregion
        public IActionResult Employee()
        {
            return View();
        }



        #region "Search Employee Leave Taken"
        [HttpGet(nameof(GetEmployeeById))]
        public ActionResult GetEmployeeById(int EmpId)
        {
            try
            {
                var EmployeeClass = EmployeeService.GetEmployeeById(EmpId);
                if (EmployeeClass != null)
                {
                    return Ok(EmployeeClass);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }

            return BadRequest("Not found");
        }
        #endregion

        

        #region "Update Employee Data"
        
        #endregion

        #region "Search Employee List"
        

        #endregion

        #region "Leave Type"
       
        public ActionResult GetAllLeaveType()
        {
            try
            {
                var LeaveDetail = EmployeeService.GetAllLeaveType();
                if (LeaveDetail != null)
                {
                    return View(LeaveDetail);
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");
        }
        #endregion
        #region "Apply Planned Leaves"
        public IActionResult ApplyPLeave()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApplyPLeave(ApplyPlannedLeave applyPlannedLeave)
        {
            try
            {
                EmployeeService.ApplyPL(applyPlannedLeave);

                return Ok("Leave Applied Successully");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");

        }
        #endregion

        #region "Cancel Planned Leaves"
        public IActionResult CancelPlannedLeave()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CancelPlannedLeave(int EmpId)
        {
            try
            {
                EmployeeService.DeletePLeave(EmpId);

                return Ok("Leave Cancelled");

            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");
        }
        #endregion
        public ActionResult GetApplication()
        {
            try
            {
                var LeaveApp = EmployeeService.GetApplication();
                if (LeaveApp != null)
                {
                    return View(LeaveApp);
                }
            }

            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");
        }

    }

}

