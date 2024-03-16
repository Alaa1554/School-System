using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Interfaces;
using MVC.Models;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class CourseResultController : Controller
    {
        private readonly ICourseResultRepository _courseResultRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITraineeRepository _traineeRepository;
        public CourseResultController(ICourseResultRepository courseResultRepository, ICourseRepository courseRepository, ITraineeRepository traineeRepository)
        {
            _courseResultRepository = courseResultRepository;
            _courseRepository = courseRepository;
            _traineeRepository = traineeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courseResults=await _courseResultRepository.GetAll();
            return View(courseResults);
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _courseResultRepository.GetById(id));
        }
        public async Task<IActionResult> GetEditView(int id)
        {
            var courseResult=await _courseResultRepository.GetById(id);
            var courses=await _courseRepository.GetAll();
            var trainees=await _traineeRepository.GetAll();
            ViewBag.Courses = courses;
            ViewBag.Trainees = trainees;
            return View("Edit",courseResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CourseResult newCourseResult)
        {
            if (ModelState.IsValid)
            {
                var course = await _courseRepository.GetById(newCourseResult.CourseId);
                if (newCourseResult.Degree > course.Degree)
                    ModelState.AddModelError("", "Degree must be less than course degree");
                else
                {
                    await _courseResultRepository.Update(id, newCourseResult);
                    return RedirectToAction("Index");
                }
            }
            var courses = await _courseRepository.GetAll();
            var trainees = await _traineeRepository.GetAll();
            ViewBag.Courses = courses;
            ViewBag.Trainees = trainees;
            return View("Edit", newCourseResult);
        }
        public async Task<IActionResult> GetView()
        {
            var courses = await _courseRepository.GetAll();
            var trainees = await _traineeRepository.GetAll();
            ViewBag.Courses = courses;
            ViewBag.Trainees = trainees;
            return View("AddNew");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourseResult(CourseResult newCourseResult)
        {
            if (ModelState.IsValid)
            {
                var course = await _courseRepository.GetById(newCourseResult.CourseId);
                if (newCourseResult.Degree > course.Degree)
                    ModelState.AddModelError("", "Degree must be less than course degree");
                else
                {
                    await _courseResultRepository.Insert(newCourseResult);
                    return RedirectToAction("Index");
                }
            }
            var courses = await _courseRepository.GetAll();
            var trainees = await _traineeRepository.GetAll();
            ViewBag.Courses = courses;
            ViewBag.Trainees = trainees;
            return View("AddNew",newCourseResult);
        }
        public async Task<IActionResult> DeleteCourseResult(int id)
        {
            await _courseResultRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
