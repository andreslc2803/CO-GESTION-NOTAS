using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Estudiante.Queries
{
    public class GetEstudiantesHandler(IEstudianteRepository _repo)
        : IRequestHandler<GetEstudiantesQuery, ApiResponse<List<GestionNotas.Api.Domain.Entities.Estudiante>>>
    {
        public async Task<ApiResponse<List<GestionNotas.Api.Domain.Entities.Estudiante>>> Handle(GetEstudiantesQuery q, CancellationToken ct)
        {
            return ApiResponse<List<GestionNotas.Api.Domain.Entities.Estudiante>>.Success(await _repo.GetAllAsync());
        }
    }
}
