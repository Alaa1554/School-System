using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Interfaces;
using MVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ApplicationDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var depts= await _departmentRepository.GetAll();
            return View(depts);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var department =await _departmentRepository.GetById(id);
            return View(department);
        }
        public IActionResult GetView()
        {
            return View("AddNew");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if(ModelState.IsValid)
            {
                await _departmentRepository.Insert(department);
                return RedirectToAction("Index");
            }
            return View("AddNew",department);
        }
        public async Task<IActionResult> GetEditView(int id)
        {
            var dept=await _departmentRepository.GetById(id);
            return View("Edit",dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Department dept)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.Update(id, dept);
                return RedirectToAction("Index");
            }
            return View("Edit",dept);
        }
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Instructors.Any(i => i.DepartId == id))
                return RedirectToAction("Index");
            await _departmentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
