using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MinLength(2,ErrorMessage = "Name must be more than 2")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must be more than 2")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50")]
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}
