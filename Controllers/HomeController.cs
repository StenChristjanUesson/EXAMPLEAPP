using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITB2203Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
