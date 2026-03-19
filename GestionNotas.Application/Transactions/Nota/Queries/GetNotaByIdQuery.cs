using GestionNotas.Api.Application.Common.Models;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Queries
{
    public sealed record GetNotaByIdQuery(int Id) : IRequest<ApiResponse<GestionNotas.Api.Domain.Entities.Nota>>;
}
