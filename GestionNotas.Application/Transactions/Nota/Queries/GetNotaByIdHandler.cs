using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Queries
{
    public class GetNotaByIdHandler(INotaRepository _repo)
        : IRequestHandler<GetNotaByIdQuery, ApiResponse<GestionNotas.Api.Domain.Entities.Nota>>
    {
        public async Task<ApiResponse<GestionNotas.Api.Domain.Entities.Nota>> Handle(GetNotaByIdQuery request, CancellationToken cancellationToken)
        {
            var nota = await _repo.GetByIdAsync(request.Id);

            if (nota == null)
                return ApiResponse<GestionNotas.Api.Domain.Entities.Nota>.Failure("Nota no encontrada");

            return ApiResponse<GestionNotas.Api.Domain.Entities.Nota>.Success(nota);
        }
    }
}
