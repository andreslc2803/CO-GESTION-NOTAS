using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Estudiante.Queries
{
    public record GetEstudiantesQuery() : IRequest<ApiResponse<List<GestionNotas.Api.Domain.Entities.Estudiante>>>;
}
