using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Commands
{
    public class CreateProfesorHandler(IProfesorRepository _repo)
        : IRequestHandler<CreateProfesorCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateProfesorCommand cmd, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(cmd.Nombre))
                return ApiResponse<int>.Failure("Nombre requerido");

            var profesor = new GestionNotas.Api.Domain.Entities.Profesor(cmd.Nombre);

            var id = await _repo.CreateAsync(profesor);

            return ApiResponse<int>.Success(id, "Profesor creado correctamente");
        }
    }
}
