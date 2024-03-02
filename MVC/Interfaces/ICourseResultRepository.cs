using MVC.Dtos;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface ICourseResultRepository
    {
        Task<IEnumerable<CourseResult>> GetAll();
        Task<CourseResult> GetById(int id);
        Task Insert(CourseResult courseResult);
        Task Update(int id, CourseResult courseResult);
        Task Delete(int id);
    }
}
