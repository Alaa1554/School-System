using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Dtos;
using MVC.Interfaces;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public InstructorRepository(ApplicationDbContext context, IImageRepository imageRepository, IMapper mapper)
        {
            _context = context;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var instructor = await GetById(id);
            _imageRepository.DeleteImage(instructor.Image);
            _context.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAll()
        {
            return await _context.Instructors.Include(i=>i.Course).Include(i=>i.Department).ToListAsync();
        }

        public async Task<Instructor> GetById(int id)
        {
           return await _context.Instructors.Include(i => i.Course).Include(i => i.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(InstructorDto instructor)
        {
            var newInstructor=_mapper.Map<Instructor>(instructor);
            newInstructor.Image = _imageRepository.UploadImage(instructor.Image);
            await _context.AddAsync(newInstructor);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, InstructorDto instructor)
        {
            var oldInstructor=await GetById(id);
            _mapper.Map(instructor, oldInstructor);
            oldInstructor.Image=instructor.Image==null?oldInstructor.Image:_imageRepository.UpdateImage(instructor.Image,oldInstructor.Image);
            await _context.SaveChangesAsync();
        }
    }
}
