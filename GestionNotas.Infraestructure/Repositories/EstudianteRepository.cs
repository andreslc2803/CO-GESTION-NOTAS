using GestionNotas.Api.Infrastructure.Persistence;
using GestionNotas.Api.Domain.Entities;
using GestionNotas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestionNotas.Infraestructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly GestionNotasDbContext _context;

        public EstudianteRepository(GestionNotasDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
            => await _context.Estudiantes.FindAsync(id);

        public async Task<int> CreateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante.Id;
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id)
                ?? throw new Exception("Estudiante no existe");

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }
    }
}
