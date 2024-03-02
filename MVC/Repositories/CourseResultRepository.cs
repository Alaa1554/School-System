using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Interfaces;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class CourseResultRepository : ICourseResultRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var courseResult = await GetById(id);
            _context.Remove(courseResult);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseResult>> GetAll()
        {
            return await _context.CoursesResults.Include(c => c.Trainee).Include(c => c.Course).ToListAsync();
        }

        public async Task<CourseResult> GetById(int id)
        {
            return await _context.CoursesResults.Include(c=>c.Trainee).Include(c=>c.Course).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Insert(CourseResult courseResult)
        {
           await _context.AddAsync(courseResult);
           await _context.SaveChangesAsync();
        }

        public async Task Update(int id, CourseResult newCourseResult)
        {
            var courseResult=await GetById(id);
            courseResult.Degree = newCourseResult.Degree;
            courseResult.TranieeId = newCourseResult.TranieeId;
            courseResult.CourseId = newCourseResult.CourseId;
            await _context.SaveChangesAsync();
        }
    }
}
