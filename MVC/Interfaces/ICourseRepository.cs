using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetByDeptId(int deptId);
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
        Task Insert(Course course);
        Task Update(int id, Course course);
        Task Delete(int id);
    }
}
