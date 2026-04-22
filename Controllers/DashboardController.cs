using Hostel_V.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hostel_V.Controllers
{

    public class DashboardController : Controller
    {
        
        public IActionResult Index()
        {
           

            //ViewBag.Name = HttpContext.Session.GetString("FullName");

            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View();
        }
    }


}


