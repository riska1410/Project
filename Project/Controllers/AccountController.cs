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
            return View(); // Menampilkan halaman form login
        }

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);

            if (user != null)
            {
                // Simpan ID ke session
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                // Redirect ke halaman dashboard
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Nama atau Password salah";
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
