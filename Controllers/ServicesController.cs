using Microsoft.AspNetCore.Mvc;

namespace Hostel_V.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
