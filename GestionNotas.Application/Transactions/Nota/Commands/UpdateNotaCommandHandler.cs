using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    internal class UpdateNotaCommandHandler(INotaRepository _repo)
    : IRequestHandler<UpdateNotaCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateNotaCommand cmd, CancellationToken cancellationToken)
        {
            var nota = await _repo.GetByIdAsync(cmd.Id);

            if (nota == null)
                return ApiResponse<bool>.Failure("Nota no existe");

            if (string.IsNullOrWhiteSpace(cmd.Nombre))
                return ApiResponse<bool>.Failure("Nombre requerido");

            if (cmd.Valor < 0 || cmd.Valor > 5)
                return ApiResponse<bool>.Failure("Valor inválido");

            if (!await _repo.ExisteProfesor(cmd.IdProfesor))
                return ApiResponse<bool>.Failure("Profesor no existe");

            if (!await _repo.ExisteEstudiante(cmd.IdEstudiante))
                return ApiResponse<bool>.Failure("Estudiante no existe");

            // actualizar
            nota = GestionNotas.Api.Domain.Entities.Nota.CreateNota(
                cmd.Nombre,
                cmd.IdProfesor,
                cmd.IdEstudiante,
                cmd.Valor
            );

            await _repo.UpdateAsync(nota);

            return ApiResponse<bool>.Success(true, "Actualizado correctamente");
        }
    }
}
