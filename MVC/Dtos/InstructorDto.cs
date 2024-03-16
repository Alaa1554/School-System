﻿using MVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace MVC.Dtos
{
    public class InstructorDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string Name { get; set; }
       
        public IFormFile Image { get; set; }
        [Required]
        [Range(2000, 10000)]
        public decimal Salary { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Name must be less than 255")]
        [MinLength(1, ErrorMessage = "Name must be more than 1")]
        public string Address { get; set; }
        [ForeignKey("Department")]
        [Display(Name = "Department")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "This field is required")]
        public int DepartId { get; set; }
        [ForeignKey("Course")]
        [Display(Name = "Course")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "This field is required")]
        public int CourseId { get; set; }
    }
}
