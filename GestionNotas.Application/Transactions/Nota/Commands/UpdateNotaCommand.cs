using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    public record UpdateNotaCommand(
        int Id,
        string Nombre,
        int IdProfesor,
        int IdEstudiante,
        decimal Valor
    ) : IRequest<ApiResponse<bool>>;
}
