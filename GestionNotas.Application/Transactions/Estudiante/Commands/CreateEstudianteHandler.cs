using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Estudiante.Commands
{
    public class CreateEstudianteHandler(IEstudianteRepository _repo)
        : IRequestHandler<CreateEstudianteCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateEstudianteCommand cmd, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(cmd.Nombre))
                return ApiResponse<int>.Failure("Nombre requerido");

            var entity = new GestionNotas.Api.Domain.Entities.Estudiante(cmd.Nombre);
            var id = await _repo.CreateAsync(entity);

            return ApiResponse<int>.Success(id);
        }
    }
}
