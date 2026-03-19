using GestionNotas.Api.Domain.Entities;

namespace GestionNotas.Domain.Interfaces.Repositories
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task<int> CreateAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(int id);
    }
}
