using Microsoft.AspNetCore.Mvc;
using MVC.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string Name { get; set; }
        [Required]
        [Range(20, 100, ErrorMessage = "Degree must be from 20 to 100")]
        public int Degree { get; set; }
        [Required]
        [Range(10, 50, ErrorMessage = "Degree must be from 10 to 50")]
        [Degree]
        [Remote("CheckDegree","validation",AdditionalFields ="Degree",ErrorMessage = "minDegree must be half of degree")]
        public int MinDegree { get; set; }
        [ForeignKey("Department")]
        [Display(Name ="Department")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DepartmentId must be more than 0")]
        public int DepartNum { get; set; }
        public Department Department { get; set; }
    }
}
