using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Profesor.Commands
{
    public class DeleteProfesorHandler(IProfesorRepository _repo)
        : IRequestHandler<DeleteProfesorCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteProfesorCommand cmd, CancellationToken ct)
        {
            await _repo.DeleteAsync(cmd.Id);
            return ApiResponse<bool>.Success(true, "Profesor eliminado");
        }
    }
}
