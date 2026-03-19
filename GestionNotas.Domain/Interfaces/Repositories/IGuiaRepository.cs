using GestionNotas.Api.Domain.Entities;

namespace GestionNotas.Api.Domain.Interfaces.Repositories;

public interface IGuiaRepository
{
    Task<Guia?> ObtenerPorNumeroGuiaAsync(string numeroGuia, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guia>> ObtenerPorRemitenteAsync(string remitente, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guia>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default);
}
