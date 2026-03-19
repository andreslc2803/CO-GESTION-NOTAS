using GestionNotas.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Domain.Interfaces.Repositories
{
    public interface IProfesorRepository
    {
        Task<List<Profesor>> GetAllAsync();
        Task<Profesor?> GetByIdAsync(int id);
        Task<int> CreateAsync(Profesor profesor);
        Task UpdateAsync(Profesor profesor);
        Task DeleteAsync(int id);
    }
}
