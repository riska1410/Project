using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _context.Users.Find(int.Parse(userId));
                ViewBag.UserName = user?.Name ?? "admin";
                ViewBag.Role = user?.Role ?? "admin";
            }

            var trainings = _context.Trainings.ToList(); 
            return View(trainings); 
        }

        public IActionResult Privacy()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _context.Users.Find(int.Parse(userId));
                ViewBag.UserName = user?.Name ?? "admin";
                ViewBag.Role = user?.Role ?? "admin";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            var trainings = _context.Trainings.ToList() ?? new List<Training>();
            return View(trainings); // ke Views/Home/Dashboard.cshtml
        }

        public IActionResult Training()
        {
            var role = HttpContext.Session.GetString("UserRole");
            ViewBag.IsAdmin = (role == "admin");

            var trainings = _context.Trainings.ToList() ?? new List<Training>();
            return View(trainings); // ke Views/Home/Training.cshtml
        }

        public IActionResult Certification()
        {
            var role = HttpContext.Session.GetString("UserRole");
            bool isAdmin = role == "admin";

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var model = new CertificationViewModel
            {
                Trainings = _context.Trainings.ToList() ?? new List<Training>(),
                Certifications = _context.Certifications.Include(c => c.Training).ToList() ?? new List<Certification>(),
                IsAdmin = isAdmin
            };

            return View("Certification", model); // ke Views/Home/Certification.cshtml
        }

        public IActionResult Resources()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            var resources = _context.Resources.ToList() ?? new List<Resource>();
            return View(resources); // ke Views/Home/Resources.cshtml
        }
    }
}
