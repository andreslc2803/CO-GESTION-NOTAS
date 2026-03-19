using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Api.Domain.Exceptions;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    public class CreateNotaCommandHandler(INotaRepository _repo) : IRequestHandler<CreateNotaCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateNotaCommand cmd, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cmd.Nombre))
                throw new BusinessException("Nombre requerido");

            if (cmd.Valor < 0 || cmd.Valor > 5)
                throw new BusinessException("Valor debe estar entre 0 y 5");

            if (!await _repo.ExisteProfesor(cmd.IdProfesor))
                throw new Exception("Profesor no existe");

            if (!await _repo.ExisteEstudiante(cmd.IdEstudiante))
                throw new Exception("Estudiante no existe");

            var nota = GestionNotas.Api.Domain.Entities.Nota.CreateNota(
                cmd.Nombre,
                cmd.IdProfesor,
                cmd.IdEstudiante,
                cmd.Valor
            );
            
            var create = await _repo.CreateAsync(nota);

            return ApiResponse<int>.Success(create, "Nota creada correctamente");
        }
    }
}
