using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Dtos;
using MVC.Interfaces;
using MVC.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TraineeController(ITraineeRepository traineeRepository, ApplicationDbContext context, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _traineeRepository = traineeRepository;
            _context = context;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var trainee=await _traineeRepository.GetById(id);
            var deptName=trainee.Department.Name;
            ViewBag.deptName = deptName;   
            return View(trainee);
        }
        public async Task<IActionResult> Index()
        {
            var trainees = await _traineeRepository.GetAll();
            return View(trainees);
        }
      
        public async Task<IActionResult> GetView()
        {
            var departments=await _departmentRepository.GetAll();
            ViewBag.Depts= departments;
            return View("AddNew");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTrainee(TraineeDto trainee)
        {
            if(ModelState.IsValid)
            {
               await _traineeRepository.Insert(trainee);
                return RedirectToAction("Index");
            }
            var departments =await _departmentRepository.GetAll();
            ViewBag.Depts= departments;
            return View("AddNew", trainee);
        }
        public async Task<IActionResult> GetEditView(int id)
        {
            var trainee=await _traineeRepository.GetById(id);
            var dto=_mapper.Map<TraineeDto>(trainee);
            var departments = await _departmentRepository.GetAll();
            ViewBag.Depts= departments;
            ViewBag.Trainee = trainee;
            return View("Edit", dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TraineeDto dto)
        {
            if (ModelState.IsValid)
            {
                await _traineeRepository.Update(id,dto);
                return RedirectToAction("Index");
            }
            ViewBag.Depts = await _departmentRepository.GetAll();
            ViewBag.Trainee = _traineeRepository.GetById(id);
            return View("Edit", dto);
        }
        public async Task<IActionResult> DeleteTrainee(int id)
        {
            if (await _context.CoursesResults.AnyAsync(x => x.TranieeId == id))
                return RedirectToAction("Index");
            
           await _traineeRepository.Delete(id);
            return RedirectToAction("Index");
        }
     
    }
}
