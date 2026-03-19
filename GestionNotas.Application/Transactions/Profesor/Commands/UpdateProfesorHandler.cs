using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;

namespace GestionNotas.Application.Transactions.Profesor.Commands
{
    public class UpdateProfesorHandler(IProfesorRepository _repo)
        : IRequestHandler<UpdateProfesorCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateProfesorCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(cmd.Id);

            if (entity == null)
                return ApiResponse<bool>.Failure("Profesor no existe");

            entity.Update(cmd.Nombre);

            await _repo.UpdateAsync(entity);

            return ApiResponse<bool>.Success(true, "Profesor actualizado");
        }
    }
}
