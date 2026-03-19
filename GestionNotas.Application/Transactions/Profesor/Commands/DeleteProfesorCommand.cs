using GestionNotas.Api.Application.Common.Models;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Commands
{
    public record DeleteProfesorCommand(int Id) : IRequest<ApiResponse<bool>>;
}
