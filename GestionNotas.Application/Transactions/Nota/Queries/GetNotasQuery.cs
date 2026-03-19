using GestionNotas.Api.Application.Common.Models;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Queries
{
    public record GetNotasQuery() : IRequest<ApiResponse<List<GestionNotas.Api.Domain.Entities.Nota>>>;
}
