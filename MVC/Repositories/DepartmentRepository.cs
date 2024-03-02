using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Interfaces;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var department = await GetById(id);
            _context.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
           return await _context.Departments.FirstOrDefaultAsync(d=>d.Id==id);
        }

        public async Task Insert(Department department)
        {
           await _context.AddAsync(department);
           await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Department department)
        {
            var dept = await GetById(id);
            dept.Name = department.Name;
            dept.Manager = department.Manager;
            await _context.SaveChangesAsync();
        }
    }
}
