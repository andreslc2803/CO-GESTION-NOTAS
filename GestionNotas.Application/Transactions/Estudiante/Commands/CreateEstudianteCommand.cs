using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Estudiante.Commands
{
    public record CreateEstudianteCommand(string Nombre)
    : IRequest<ApiResponse<int>>;
}
