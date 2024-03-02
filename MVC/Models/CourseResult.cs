using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class CourseResult
    {
        public int Id { get; set; }
        [Required]
        [Range(0,100, ErrorMessage = "Degree must be from 0 to 100")]
        public decimal Degree { get; set; }
        [ForeignKey("Trainee")]
        [Required]
        [Display(Name ="Trainee")]
        public int TranieeId { get; set; }
        public Trainee Trainee { get; set; }
        [ForeignKey("Course")]
        [Required]
        [Display(Name ="Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
