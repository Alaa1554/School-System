using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class ValidationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ValidationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult CheckDegree(int MinDegree, int Degree)
        {

            if (MinDegree != (Degree * 0.5))
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}
