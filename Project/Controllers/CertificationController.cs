using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class CertificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CertificationController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        // GET: /Certification
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            bool isAdmin = role == "admin";

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var model = new CertificationViewModel
            {
                Trainings = _context.Trainings.ToList(),
                Certifications = _context.Certifications.Include(c => c.Training).ToList(),
                IsAdmin = isAdmin
            };

            // 💥 INI YANG PENTING → render view dari folder Home!
            return View("~/Views/Home/Certification.cshtml", model);
        }


        // POST: /Certification/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int trainingId)
        {
            if (file != null && file.Length > 0)
            {
                var folder = Path.Combine(_env.WebRootPath, "certificates");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Cek jika sudah ada sertifikat untuk training ini
                var existing = _context.Certifications.FirstOrDefault(c => c.TrainingId == trainingId);
                if (existing != null)
                {
                    existing.FilePath = "/certificates/" + fileName;
                    _context.Update(existing);
                }
                else
                {
                    var cert = new Certification
                    {
                        TrainingId = trainingId,
                        FilePath = "/certificates/" + fileName
                    };
                    _context.Certifications.Add(cert);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: /Certification/Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cert = _context.Certifications.Find(id);
            if (cert != null)
            {
                var fullPath = Path.Combine(_env.WebRootPath, cert.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                _context.Certifications.Remove(cert);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");


        }
    }
}
