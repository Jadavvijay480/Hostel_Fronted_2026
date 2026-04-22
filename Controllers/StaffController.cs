using Microsoft.AspNetCore.Mvc;

namespace Hostel_V.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
