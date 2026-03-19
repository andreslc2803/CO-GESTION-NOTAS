using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Application.Transactions.Estudiante.Commands
{
    public class UpdateEstudianteHandler(IEstudianteRepository _repo)
        : IRequestHandler<UpdateEstudianteCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateEstudianteCommand cmd, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(cmd.Id);
            if (e == null) return ApiResponse<bool>.Failure("No existe");

            e.Update(cmd.Nombre);

            await _repo.UpdateAsync(e);
            return ApiResponse<bool>.Success(true);
        }
    }
}
