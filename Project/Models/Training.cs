using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Judul wajib diisi")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Deskripsi wajib diisi")]
        public string Description { get; set; }

        public string IconClass { get; set; }

        // Relasi ke Sertifikat
        public List<Certification>? Certifications { get; set; }

    }
}
