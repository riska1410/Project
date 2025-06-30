using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Project.Models;
using Project.Data;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _context.Users.Find(int.Parse(userId));
                ViewBag.UserName = user?.Name ?? "Admin";
                ViewBag.Role = user?.Role ?? "Admin";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _context.Users.Find(int.Parse(userId));
                ViewBag.UserName = user?.Name ?? "Admin";
                ViewBag.Role = user?.Role ?? "Admin";
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
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            return View();
        }

        public IActionResult Training()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name ?? "Admin";
            ViewBag.Role = user?.Role ?? "Admin";

            return View();
        }

        public IActionResult Certification()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            return View();
        }

        public IActionResult Resources()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            return View();
        }
    }
}
