using MVC.Dtos;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAll();
        Task<Instructor> GetById(int id);
        Task Insert(InstructorDto instructor);
        Task Update(int id, InstructorDto instructor);
        Task Delete(int id);
    }
}
