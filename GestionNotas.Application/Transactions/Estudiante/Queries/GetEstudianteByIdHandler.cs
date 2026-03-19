using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Estudiante.Queries
{
    public class GetEstudianteByIdHandler(IEstudianteRepository _repo)
        : IRequestHandler<GetEstudianteByIdQuery, ApiResponse<GestionNotas.Api.Domain.Entities.Estudiante>>
    {
        public async Task<ApiResponse<GestionNotas.Api.Domain.Entities.Estudiante>> Handle(GetEstudianteByIdQuery q, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(q.Id);
            return e == null
                ? ApiResponse<GestionNotas.Api.Domain.Entities.Estudiante>.Failure("No existe")
                : ApiResponse<GestionNotas.Api.Domain.Entities.Estudiante>.Success(e);
        }
    }
}
