using System.Collections.Generic;

namespace Project.Models
{
    public class CertificationViewModel
    {
        public List<Training> Trainings { get; set; }
        public List<Certification> Certifications { get; set; }
        public bool IsAdmin { get; set; }
    }
}
