using GestionNotas.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Domain.Interfaces.Repositories
{
    public interface INotaRepository
    {
        Task<List<Nota>> GetAllAsync();
        Task<Nota?> GetByIdAsync(int id);
        Task<int> CreateAsync(Nota nota);
        Task UpdateAsync(Nota nota);
        Task DeleteAsync(int id);

        Task<bool> ExisteProfesor(int idProfesor);
        Task<bool> ExisteEstudiante(int idEstudiante);
    }
}
