using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Profesor.Commands
{
    public record UpdateProfesorCommand(int Id, string Nombre) : IRequest<ApiResponse<bool>>;
}
