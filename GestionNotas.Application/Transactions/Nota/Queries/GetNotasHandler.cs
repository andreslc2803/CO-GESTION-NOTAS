using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Queries
{
    public sealed class GetNotasHandler(INotaRepository _repo)
        : IRequestHandler<GetNotasQuery, ApiResponse<List<GestionNotas.Api.Domain.Entities.Nota>>>
    {
        public async Task<ApiResponse<List<GestionNotas.Api.Domain.Entities.Nota>>> Handle(GetNotasQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.GetAllAsync();
            return ApiResponse<List<GestionNotas.Api.Domain.Entities.Nota>>.Success(data);
        }
    }
}
