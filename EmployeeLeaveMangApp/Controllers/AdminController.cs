using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveMangApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
