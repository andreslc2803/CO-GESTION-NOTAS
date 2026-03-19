using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Queries
{
    public class GetProfesorByIdHandler(IProfesorRepository _repo)
        : IRequestHandler<GetProfesorByIdQuery, ApiResponse<GestionNotas.Api.Domain.Entities.Profesor>>
    {
        public async Task<ApiResponse<GestionNotas.Api.Domain.Entities.Profesor>> Handle(GetProfesorByIdQuery q, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(q.Id);

            return entity == null
                ? ApiResponse<GestionNotas.Api.Domain.Entities.Profesor>.Failure("Profesor no existe")
                : ApiResponse<GestionNotas.Api.Domain.Entities.Profesor>.Success(entity);
        }
    }
}
