using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Estudiante.Commands
{
    public class DeleteEstudianteHandler(IEstudianteRepository _repo)
        : IRequestHandler<DeleteEstudianteCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteEstudianteCommand cmd, CancellationToken ct)
        {
            await _repo.DeleteAsync(cmd.Id);
            return ApiResponse<bool>.Success(true);
        }
    }
}
