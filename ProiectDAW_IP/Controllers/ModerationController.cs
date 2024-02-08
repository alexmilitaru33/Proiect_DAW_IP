using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW_IP.ContextModels;
using ProiectDAW_IP.Enums;
using ProiectDAW_IP.Models;

namespace ProiectDAW_IP.Controllers
{
    public class ModerationController : Controller
    {
        private readonly ApplicationContext _context; // Replace YourDbContext with your actual DbContext

        public ModerationController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.UserTypes = Enum.GetValues(typeof(UserType))
                .Cast<UserType>()
                .Select(t => new SelectListItem
                     {
                         Value = ((int)t).ToString(),
                         Text = t.ToString()
                     })
                     .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.UserTypes = Enum.GetValues(typeof(UserType))
                .Cast<UserType>()
                .Select(t => new SelectListItem
                {
                    Value = ((int)t).ToString(),
                    Text = t.ToString()
                })
                     .ToList();
            var user = _context.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult ViewAll()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}

