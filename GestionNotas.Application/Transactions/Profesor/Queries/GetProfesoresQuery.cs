using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Profesor.Queries
{
    public record GetProfesoresQuery() : IRequest<ApiResponse<List<GestionNotas.Api.Domain.Entities.Profesor>>>;
}
