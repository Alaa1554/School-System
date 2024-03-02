using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Interfaces;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var course = await GetById(id);
            _context.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.Include(c=>c.Department).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.Include(c=>c.Department).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Insert(Course course)
        {
           await _context.AddAsync(course);
           await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Course course)
        {
            var oldCourse=await GetById(id);
            oldCourse.Name = course.Name;
            oldCourse.MinDegree = course.MinDegree;
            oldCourse.Degree = course.Degree;
            oldCourse.DepartNum = course.DepartNum;
            await _context.SaveChangesAsync();
        }
    }
}
