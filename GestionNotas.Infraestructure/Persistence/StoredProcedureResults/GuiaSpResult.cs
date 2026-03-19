namespace GestionNotas.Api.Infrastructure.Persistence.StoredProcedureResults;

public class GuiaSpResult
{
    public int Id { get; set; }
    public string NumeroGuia { get; set; } = string.Empty;
    public string Remitente { get; set; } = string.Empty;
    public string Destinatario { get; set; } = string.Empty;
    public string CiudadOrigen { get; set; } = string.Empty;
    public string CiudadDestino { get; set; } = string.Empty;
    public decimal PesoKg { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
}
