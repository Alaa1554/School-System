using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string Name { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string Manager { get; set; }
    }
}
