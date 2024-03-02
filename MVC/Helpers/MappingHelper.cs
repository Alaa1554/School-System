using AutoMapper;
using MVC.Dtos;
using MVC.Models;

namespace MVC.Helpers
{
    public class MappingHelper:Profile
    {
        public MappingHelper()
        {
            CreateMap<InstructorDto, Instructor>().ForMember(c => c.Image, i => i.Ignore());
            CreateMap<Instructor, InstructorDto>().ForMember(c => c.Image, i => i.Ignore());
            CreateMap<TraineeDto,Trainee>().ForMember(c => c.Image,i=>i.Ignore());
            CreateMap<Trainee,TraineeDto>().ForMember(c => c.Image,i=>i.Ignore());
        }
    }
}
