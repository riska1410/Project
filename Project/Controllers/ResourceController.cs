using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var resources = _context.Resources.ToList();
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            return View("~/Views/Home/Resources.cshtml", resources);
        }
        // GET: Create Page
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Resource
        [HttpPost]
        public IActionResult Create(Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Resources.Add(resource);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource);
        }

        // GET: Edit Page
        public IActionResult Edit(int id)
        {
            var resource = _context.Resources.Find(id);
            if (resource == null) return NotFound();
            return View(resource);
        }

        // POST: Edit Submit
        [HttpPost]
        public IActionResult Edit(Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Resources.Update(resource);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var res = _context.Resources.Find(id);
            if (res != null)
            {
                _context.Resources.Remove(res);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}