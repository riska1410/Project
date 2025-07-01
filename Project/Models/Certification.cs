// 1. Model Certification.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Certification
    {
        public int Id { get; set; }

        [Required]
        public int TrainingId { get; set; }

        [ForeignKey("TrainingId")]
        public Training Training { get; set; } = default!;

        public string FilePath { get; set; } = string.Empty;
    }
}
