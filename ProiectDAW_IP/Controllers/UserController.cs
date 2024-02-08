using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW_IP.ContextModels;
using ProiectDAW_IP.Enums;
using ProiectDAW_IP.Models;

namespace ProiectDAW_IP.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {

            if (HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {

            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == newUser.UserName);

            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(newUser);
            }


            if (ModelState.IsValid)
            {
                // Add user to the database
                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect to login or dashboard upon successful registration
                return RedirectToAction("Login");
            }

            // If model state is not valid, return to registration page
            return View(newUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store");
            HttpContext.Response.Headers.Add("Expires", "-1");
            HttpContext.Response.Headers.Add("Pragma", "no-cache");
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null && user.Password == password)
            {
                // Store the username in session upon successful login
                HttpContext.Session.SetString("UserName", username);
                return RedirectToAction("Dashboard");
            }

            // If authentication fails, return to login page with an error message
            ModelState.AddModelError(string.Empty, "Username sau parola invalide!");
            return View();
        }


        public IActionResult Dashboard()
        {

            HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store";
            HttpContext.Response.Headers["Expires"] = "-1";
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login");
            }
            var username = HttpContext.Session.GetString("UserName");


            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            var userScore = user?.Score ?? 0;

            var availableSections = _context.Categorii.Where(s => s.RequiredPoints <= userScore).ToList();


            if (user != null)
            {
                var userType = user.UserType;

                bool responseUserType = false;
                bool responseUserNotAdmin = false;

                if (userType == UserType.Client)

                {
                    responseUserType = true;

                }
                if (userType != UserType.Admin)

                {
                    responseUserNotAdmin = true;

                }

                ViewBag.responseUserType = responseUserType;
                ViewBag.responseUserAdmin = responseUserNotAdmin;
            }


            return View(availableSections);


        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return RedirectToAction("Login");
        }



        public IActionResult Leaderboard(string sortOrder, string searchUserName)
        {
            IQueryable<User> usersQuery = _context.Users.Where(u => u.UserType == UserType.Client);

            // Search by username if the searchUserName parameter is not null or empty
            if (!string.IsNullOrEmpty(searchUserName))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(searchUserName));
            }

            // Sort by score or username based on the sortOrder parameter
            switch (sortOrder)
            {
                case "UserName":
                    usersQuery = usersQuery.OrderBy(u => u.UserName);
                    break;
                case "Score":
                default:
                    usersQuery = usersQuery.OrderByDescending(u => u.Score);
                    break;
            }

            List<User> sortedUsers = usersQuery.ToList();
            return View(sortedUsers);
        }



     


        
    }

}
