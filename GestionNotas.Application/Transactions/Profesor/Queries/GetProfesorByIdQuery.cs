using GestionNotas.Api.Application.Common.Models;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Queries
{
    public record GetProfesorByIdQuery(int Id) : IRequest<ApiResponse<GestionNotas.Api.Domain.Entities.Profesor>>;
}
