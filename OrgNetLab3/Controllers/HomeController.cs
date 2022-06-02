using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrgNetLab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult Student()
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Teacher()
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Lessons()
        {
            return View();
        }
    }
}
