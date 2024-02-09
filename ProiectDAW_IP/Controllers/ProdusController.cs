using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectDAW_IP.ContextModels;
using ProiectDAW_IP.Enums;
using ProiectDAW_IP.Models;
using System.Drawing;

namespace ProiectDAW_IP.Controllers
{
    public class ProdusController : Controller
    {
        private readonly ApplicationContext _context; // Replace YourDbContext with your actual DbContext

        public ProdusController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categorie = _context.Categorii.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Produs produse)
        {
            _context.Produse.Add(produse);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.categorie= _context.Categorii.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            var produs = _context.Produse.Find(id);
            return View(produs);
        }

        [HttpPost]
        public IActionResult Edit(Produs produse)
        {
            //// Put a breakpoint here and inspect ModelState during debugging
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.sectiune = _context.Categorii.Select(x => new SelectListItem { Text = x.Nume, Value = x.Id.ToString() }).ToList();

            //    // Inspect ModelState errors
            //    foreach (var modelState in ModelState.Values)
            //    {
            //        foreach (var error in modelState.Errors)
            //        {
            //            // Log or debug the error messages
            //            Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            //        }
            //    }

            //    return View(produse);
            //}

            // If the model is valid, update the entity and save changes
            _context.Produse.Update(produse);
            _context.SaveChanges();

            return RedirectToAction("Dashboard", "User");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var produse = _context.Produse.Find(id);
            _context.Produse.Remove(produse);
            _context.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult ViewAll()
        {
            var produse = _context.Produse.Include(s=>s.Categorie).ToList();
            return View(produse);
        }


        //[HttpGet]
        //public IActionResult ViewAllUsers()
        //{
        //    var produse = _context.Produse.
        //    return View(produse);
        //}
        public IActionResult ViewAllUsers(string sortOrder)
        {
            // Sortare în funcție de valoarea parametrului sortOrder
            IQueryable<Produs> productsQuery = _context.Produse;

            switch (sortOrder)
            {
                case "PretDescrescator":
                    productsQuery = productsQuery.Include(p => p.Categorie).OrderByDescending(p => p.Pret);
                    break;
                case "PretCrescator":
                    productsQuery = productsQuery.Include(p => p.Categorie).OrderBy(p => p.Pret);
                    break;

                default: // Implicit, doar afisare
                    productsQuery = productsQuery.Include(s => s.Categorie);
                    break;
            }

            var products = productsQuery.ToList();
            return View(products);
        }
    }






    //public IActionResult ViewAllUsers(string sortOrder, string searchUserName)
    //{
    //    IQueryable<User> usersQuery = _context.Users.Where(u => u.UserType == UserType.Client);

    //    // Search by username if the searchUserName parameter is not null or empty
    //    if (!string.IsNullOrEmpty(searchUserName))
    //    {
    //        usersQuery = usersQuery.Where(u => u.UserName.Contains(searchUserName));
    //    }

    //    // Sort by score or username based on the sortOrder parameter
    //    switch (sortOrder)
    //    {
    //        case "ProdusName":
    //            usersQuery = usersQuery.OrderBy(p => p.);
    //            break;
    //        case "Score":
    //        default:
    //            usersQuery = usersQuery.OrderByDescending(u => u.Score);
    //            break;
    //    }

    //    List<User> sortedUsers = usersQuery.ToList();
    //    return View(sortedUsers);
    //}


    //public IActionResult PretDescrescator()
    //    {
    //        var ProduseDesc = _context.Produse.Include(c => c.Categorie).OrderByDescending(c => c.Pret).ToList();

    //        return View(ProduseDesc);
    //    }

    //    public IActionResult PretCrescator()
    //    {
    //        var ProduseDesc = _context.Produse.Include(c => c.Categorie).OrderBy(c => c.Pret).ToList();

    //        return View(ProduseDesc);
    //    }






    
}
