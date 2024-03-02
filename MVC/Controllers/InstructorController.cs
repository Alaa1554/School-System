using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Dtos;
using MVC.Interfaces;
using MVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var Instructors =await _instructorRepository.GetAll();
            return View("Index",Instructors);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var Instructor =await _instructorRepository.GetById(id);
            return View("Detail",Instructor);
        }
        public async Task<IActionResult> GetView()
        {
            var departments= await _departmentRepository.GetAll();
            var courses= await _courseRepository.GetAll();
            ViewBag.Departments = departments;
            ViewBag.Courses = courses;
            return View("AddNew");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInstructor(InstructorDto instructor)
        {
            if (ModelState.IsValid)
            {
                await _instructorRepository.Insert(instructor);
                return RedirectToAction("Index");
            }
            var departments = await _departmentRepository.GetAll();
            var courses = await _courseRepository.GetAll();
            ViewBag.Departments = departments;
            ViewBag.Courses = courses;
            return View("AddNew",instructor);
        }
        public async Task<IActionResult> GetEditView(int id)
        {
            var instructor=await _instructorRepository.GetById(id);
            InstructorDto dto=new InstructorDto();
            _mapper.Map(instructor, dto);
            var departs= await _departmentRepository.GetAll();
            var courses= await _courseRepository.GetAll();
            ViewBag.Departments = departs;
            ViewBag.Courses = courses;
            ViewBag.Instructor=instructor;
            return View("Edit",dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,InstructorDto newInstructor)
        {
            if(ModelState.IsValid)
            {
               await _instructorRepository.Update(id, newInstructor);
                return RedirectToAction("Index");
            }
            var departments = await _departmentRepository.GetAll();
            var courses = await _courseRepository.GetAll();
            ViewBag.Departments = departments;
            ViewBag.Courses = courses;
            ViewBag.Instructor = await _instructorRepository.GetById(id);
            return View("Edit",newInstructor);
        }
        public async Task<IActionResult> DeleteInstructor(int id)
        {
           await _instructorRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
