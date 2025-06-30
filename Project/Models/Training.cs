using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string IconClass { get; set; }  // contoh: "fas fa-cogs"
    }
}
