using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace MVC.Dtos
{
    public class TraineeDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Name must be less than 255")]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        public string Address { get; set; }
        [Required]
        [Range(0.1, 4, ErrorMessage = "Grade must be from 0.1 to 4")]
        public decimal Grade { get; set; }
        [ForeignKey("Department")]
        [Display(Name = "Department")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DepartmentId must be more than 0")]
        public int DepartNum { get; set; }
    }
}
