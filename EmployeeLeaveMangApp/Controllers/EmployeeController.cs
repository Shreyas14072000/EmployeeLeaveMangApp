using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using EmployeeLeaveMangApp.Models;
using EmployeeLeaveMangApp.Infrastructure;
using System.Threading.Tasks;

namespace EmployeeLeaveMangApp.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly InterfaceEmployeeService EmployeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly SendServiceBusMessage _sendServiceBusMessage;


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



        ////#region "Search Employee Leave Taken"
        ////public IActionResult GetEmployeeById()
        ////{
        ////    return View();
        ////}
        ////[HttpPost]
        ////public IActionResult GetEmployeeById(int EmpId)
        ////{
        ////    try
        ////    {
        ////        var EmployeeClass = EmployeeService.GetEmployeeById(EmpId);
        ////        if (EmployeeClass != null)
        ////        {
        ////            return Ok(EmployeeClass);
        ////        }
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        _logger.LogError("Exception Occured", e.InnerException);
        ////    }

        ////    return BadRequest("Not found");
        ////}
        //#endregion

        

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
        public async Task<IActionResult> ApplyPLeave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyPLeave(ApplyPlannedLeave applyPlannedLeave)
        {
            try
            {
                EmployeeService.ApplyPL(applyPlannedLeave);
                await _sendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {

                    EmpId = applyPlannedLeave.EmpId,
                    EmpName = applyPlannedLeave.EmpName,
                    LeaveDuration = applyPlannedLeave.LeaveDuration,
                    LeaveReason = applyPlannedLeave.LeaveReason


                });

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
        public async Task<IActionResult> CancelPlannedLeave()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CancelPlannedLeave(int EmpId)
        {
            try
            {
                EmployeeService.DeletePLeave(EmpId);
                await _sendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    EmpId = EmpId

                }) ;

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

