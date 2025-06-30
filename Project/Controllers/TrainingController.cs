using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Project.Models;
using Project.Data;
using System.Linq;

namespace Project.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserRole()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return null;

            var user = _context.Users.Find(int.Parse(userId));
            ViewBag.UserName = user?.Name;
            ViewBag.Role = user?.Role;
            return user?.Role;
        }

        private bool IsAdmin()
        {
            return GetUserRole() == "Admin";
        }

        public IActionResult Index()
        {
            var trainings = _context.Trainings.ToList();
            ViewBag.IsAdmin = IsAdmin();
            return View(trainings);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return Unauthorized();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Training training)
        {
            if (!IsAdmin()) return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Trainings.Add(training);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(training);
        }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var training = _context.Trainings.Find(id);
            if (training == null) return NotFound();

            return View(training);
        }

        [HttpPost]
        public IActionResult Edit(Training training)
        {
            if (!IsAdmin()) return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Trainings.Update(training);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(training);
        }

        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var training = _context.Trainings.Find(id);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
