using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task Insert(Department department);
        Task Update(int id, Department department);
        Task Delete(int id);
    }
}
