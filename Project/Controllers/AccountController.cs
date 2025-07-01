using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Project.Data;
using System.Linq;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role?.ToLower() ?? "employee");
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Email atau password salah";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;

            return View(user);
        }
    }
}
