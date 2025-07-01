using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }  // <-- HARUS COCOK DENGAN DATABASE
    }


}

