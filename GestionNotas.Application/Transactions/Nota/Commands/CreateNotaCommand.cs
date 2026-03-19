using GestionNotas.Api.Application.Common.Models;
using MediatR;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    public sealed record CreateNotaCommand(
        string Nombre,
        int IdProfesor,
        int IdEstudiante,
        decimal Valor
    ) : IRequest<ApiResponse<int>>;
}
