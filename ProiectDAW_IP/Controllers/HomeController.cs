using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectDAW_IP.ContextModels;
using ProiectDAW_IP.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProiectDAW_IP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;



        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            // Verificați aici starea de autentificare a utilizatorului
            bool isUserLoggedIn = false;

            if (HttpContext.Session.GetString("UserName") != null)
            {
                isUserLoggedIn = true;
            }

            // Transmiteți starea de autentificare către vedere
            ViewBag.IsUserLoggedIn = isUserLoggedIn;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
