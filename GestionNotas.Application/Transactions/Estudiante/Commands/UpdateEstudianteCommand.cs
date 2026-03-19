using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Estudiante.Commands
{
    public record UpdateEstudianteCommand(int Id, string Nombre) : IRequest<ApiResponse<bool>>;
}
