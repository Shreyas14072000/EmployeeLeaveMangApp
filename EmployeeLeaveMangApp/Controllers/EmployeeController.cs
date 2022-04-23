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
        public EmployeeController(InterfaceEmployeeService EmployeeService, ILogger<EmployeeController> logger, SendServiceBusMessage sendServiceBusMessage)
        {
            _logger = logger;
            _logger.LogInformation("Employee Added");
            this.EmployeeService = EmployeeService;
            _sendServiceBusMessage = sendServiceBusMessage;
        }
        #endregion
        public IActionResult Employee()
        {
            return View();
        }







        #region "Update Employee Data"

        #endregion

        #region "Search Employee List"


        #endregion

        #region "Leave Type"
        public async Task<IActionResult> GetAllLeaveType()
        {
            _logger.LogInformation("student endpoint starts");
            var Leave = await EmployeeService.GetAllLeaveType();
            try
            {
                if (Leave == null) return NotFound();
                _logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
            //return Ok(student);
            return View(Leave);
        }
        #endregion
        #region "Apply Planned Leaves"
        public IActionResult ApplyPLeave()
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
                    LeaveReason = applyPlannedLeave.LeaveReason,
                    Action = "Applied For Planned  Leave"


                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            ViewBag.Message = string.Format("Applied for Leave successfully");
            return View();
        }
        #endregion

        #region "Cancel Planned Leaves"
        public IActionResult CancelPlannedLeave()
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
                    EmpId = EmpId,
                    Action = "Cancalled the Leave"

                }) ;


            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            ViewBag.Message = string.Format("Leave Cancelled Successfully");
            return View();
        }
        #endregion
        public async Task<IActionResult> GetApplication()
        {
            _logger.LogInformation("student endpoint starts");
            var Leave = await EmployeeService.GetApplication();
            try
            {
                if (Leave == null) return NotFound();
                _logger.LogInformation("student endpoint completed");
            }
            catch (Exception ex)
            {
                _logger.LogError("exception occured;ExceptionDetail:" + ex.Message);
                _logger.LogError("exception occured;ExceptionDetail:" + ex.InnerException);
                _logger.LogError("exception occured;ExceptionDetail:" + ex);
                return BadRequest();
            }
            //return Ok(student);
            return View(Leave);
        }
    }

    }



