using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    internal class DeleteNotaCommandHandler(INotaRepository _repo)
    : IRequestHandler<DeleteNotaCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteNotaCommand cmd, CancellationToken cancellationToken)
        {
            var nota = await _repo.GetByIdAsync(cmd.Id);

            if (nota == null)
                return ApiResponse<bool>.Failure("Nota no existe");

            await _repo.DeleteAsync(cmd.Id);

            return ApiResponse<bool>.Success(true, "Eliminado correctamente");
        }
    }
}
