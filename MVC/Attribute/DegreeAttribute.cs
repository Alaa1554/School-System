using MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC.Attribute
{
    public class DegreeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int minDegree = (int)value;
            var course = validationContext.ObjectInstance as Course;
            if (course != null)
            {
                if(minDegree!=(course.Degree*0.5))
                {
                    return new ValidationResult("minDegree must be half of degree");
                }
            }
            return ValidationResult.Success;
        }
    }
}
