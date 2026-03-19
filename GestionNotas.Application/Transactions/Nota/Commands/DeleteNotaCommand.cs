using GestionNotas.Api.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Nota.Commands
{
    public record DeleteNotaCommand(int Id) : IRequest<ApiResponse<bool>>;
}
