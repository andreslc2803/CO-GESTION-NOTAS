using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Queries
{
    public class GetProfesoresHandler(IProfesorRepository _repo)
        : IRequestHandler<GetProfesoresQuery, ApiResponse<List<GestionNotas.Api.Domain.Entities.Profesor>>>
    {
        public async Task<ApiResponse<List<GestionNotas.Api.Domain.Entities.Profesor>>> Handle(GetProfesoresQuery q, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync();
            return ApiResponse<List<GestionNotas.Api.Domain.Entities.Profesor>>.Success(list);
        }
    }
}
