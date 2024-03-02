using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Interfaces;
using MVC.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
       private readonly ICourseRepository _courseRepository;
       private readonly IDepartmentRepository _departmentRepository;
        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses =await _courseRepository.GetAll();
            return View(courses);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var course=await _courseRepository.GetById(id);
            var deptName=course.Department.Name;
            ViewBag.deptName = deptName;
            return View(course);
        }
        public async Task<IActionResult> GetEditView(int id)
        {
            var course = await _courseRepository.GetById(id);
            var depts=await _departmentRepository.GetAll();
            ViewBag.depts = depts;
            return View("Edit",course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Course course)
        {
            if(ModelState.IsValid)
            {
                await _courseRepository.Update(id, course);
                return RedirectToAction("Index");
            }
            var depts = await _departmentRepository.GetAll();
            ViewBag.depts = depts;
            return View("Edit", course);
        }
        public async Task<IActionResult> GetView()
        {
            var depts =await _departmentRepository.GetAll();
            ViewBag.depts = depts;
            return View("AddNew");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(Course newCourse) 
        {
            if(ModelState.IsValid)
            {
                await _courseRepository.Insert(newCourse);
                return RedirectToAction("Index");
            }
            var depts = await _departmentRepository.GetAll();
            ViewBag.depts = depts;
            return View("AddNew", newCourse);
        }
        public async Task<IActionResult> Deletecourse(int id)
        {
            await _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }
       
    }
}
