using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using GestionNotas.Api.Domain.Entities;
using GestionNotas.Api.Domain.Interfaces.Repositories;
using GestionNotas.Api.Infrastructure.Persistence;
using GestionNotas.Api.Infrastructure.Persistence.StoredProcedureResults;

namespace GestionNotas.Api.Infrastructure.Repositories;

public class GuiaRepository(ServiciosDbContext context) : IGuiaRepository
{
    public async Task<Guia?> ObtenerPorNumeroGuiaAsync(string numeroGuia, CancellationToken cancellationToken = default)
    {
        var parametro = new SqlParameter("@NumeroGuia", numeroGuia);

        var resultados = await context.Database
            .SqlQuery<GuiaSpResult>($"EXEC sp_ObtenerGuiaPorNumero @NumeroGuia={numeroGuia}")
            .ToListAsync(cancellationToken);

        var resultado = resultados.FirstOrDefault();
        if (resultado is null)
            return null;

        return MapearAGuia(resultado);
    }

    public async Task<IReadOnlyList<Guia>> ObtenerPorRemitenteAsync(string remitente, CancellationToken cancellationToken = default)
    {
        var resultados = await context.Database
            .SqlQuery<GuiaSpResult>($"EXEC sp_ObtenerGuiasPorRemitente @Remitente={remitente}")
            .ToListAsync(cancellationToken);

        return resultados.Select(MapearAGuia).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyList<Guia>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default)
    {
        var resultados = await context.Database
            .SqlQuery<GuiaSpResult>($"EXEC sp_ObtenerGuiasPorEstado @Estado={estado}")
            .ToListAsync(cancellationToken);

        return resultados.Select(MapearAGuia).ToList().AsReadOnly();
    }

    private static Guia MapearAGuia(GuiaSpResult resultado)
    {
        var guia = Guia.Create(
            resultado.NumeroGuia,
            resultado.Remitente,
            resultado.Destinatario,
            resultado.CiudadOrigen,
            resultado.CiudadDestino,
            resultado.PesoKg
        );

        guia.ActualizarEstado(resultado.Estado);

        if (resultado.Estado == "ENTREGADA")
            guia.MarcarEntregada();

        return guia;
    }
}
