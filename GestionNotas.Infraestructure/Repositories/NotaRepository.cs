using GestionNotas.Api.Domain.Entities;
using GestionNotas.Api.Infrastructure.Persistence;
using GestionNotas.Api.Domain.Entities;
using GestionNotas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestionNotas.Infraestructure.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly GestionNotasDbContext _context;

        public NotaRepository(GestionNotasDbContext context)
        {
            _context = context;
        }

        public async Task<List<Nota>> GetAllAsync()
            => await _context.Notas
                .Include(n => n.Profesor)
                .Include(n => n.Estudiante)
                .ToListAsync();

        public async Task<Nota?> GetByIdAsync(int id)
            => await _context.Notas
                .Include(n => n.Profesor)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(n => n.Id == id);

        public async Task<int> CreateAsync(Nota nota)
        {
            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();
            return nota.Id;
        }

        public async Task UpdateAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id)
                ?? throw new Exception("Nota no existe");

            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteProfesor(int idProfesor)
            => await _context.Profesores.AnyAsync(p => p.Id == idProfesor);

        public async Task<bool> ExisteEstudiante(int idEstudiante)
            => await _context.Estudiantes.AnyAsync(e => e.Id == idEstudiante);
    }
}
