using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementService.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet, Route("employees/get")]
        //public IEnumerable<Curensly> sdfsd()
        //{

        //}

    }
}
