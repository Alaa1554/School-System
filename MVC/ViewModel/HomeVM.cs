using MVC.Models;
using System.Collections.Generic;

namespace MVC.ViewModel
{
    public class HomeVM
    {
       public List<Course> Courses { get; set; }
       public List<Trainee> Trainees { get; set; }
       public List<Instructor> Instructors { get; set; }
    }
}
