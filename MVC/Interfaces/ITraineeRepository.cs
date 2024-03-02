using MVC.Dtos;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface ITraineeRepository
    {
       Task<IEnumerable<Trainee>> GetAll();
       Task<Trainee> GetById(int id);
       Task Insert(TraineeDto trainee);
       Task Update(int id,TraineeDto trainee);
       Task Delete(int id);

    }
}
