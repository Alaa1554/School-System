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
    public class TraineeRepository : ITraineeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public TraineeRepository(ApplicationDbContext context, IImageRepository imageRepository, IMapper mapper)
        {
            _context = context;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        public async Task Delete(int id)
        {
            var trainee= await GetById(id);
            _imageRepository.DeleteImage(trainee.Image);
            _context.Remove(trainee);
           await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Trainee>> GetAll()
        {
            return await _context.Trainees.Include(t=>t.Department).ToListAsync();
        }

        public async Task<Trainee> GetById(int id)
        {
            return await _context.Trainees.Include(t=>t.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(TraineeDto trainee)
        { 
            var newTrainee=_mapper.Map<Trainee>(trainee);
            newTrainee.Image = _imageRepository.UploadImage(trainee.Image);
            await _context.AddAsync(newTrainee);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, TraineeDto trainee)
        {
            var oldTrainee=await GetById(id);
            _mapper.Map(trainee, oldTrainee);
            oldTrainee.Image=trainee.Image==null?oldTrainee.Image:_imageRepository.UpdateImage(trainee.Image, oldTrainee.Image);
            await _context.SaveChangesAsync();
        }
    }
}
