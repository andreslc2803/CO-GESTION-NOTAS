namespace GestionNotas.Api.Domain.Entities;

public class Guia
{
    public int Id { get; private set; }
    public string NumeroGuia { get; private set; }
    public string Remitente { get; private set; }
    public string Destinatario { get; private set; }
    public string CiudadOrigen { get; private set; }
    public string CiudadDestino { get; private set; }
    public decimal PesoKg { get; private set; }
    public string Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaEntrega { get; private set; }

    private Guia()
    {
        NumeroGuia = string.Empty;
        Remitente = string.Empty;
        Destinatario = string.Empty;
        CiudadOrigen = string.Empty;
        CiudadDestino = string.Empty;
        Estado = string.Empty;
    }

    public static Guia Create(
        string numeroGuia,
        string remitente,
        string destinatario,
        string ciudadOrigen,
        string ciudadDestino,
        decimal pesoKg)
    {
        return new Guia
        {
            NumeroGuia = numeroGuia,
            Remitente = remitente,
            Destinatario = destinatario,
            CiudadOrigen = ciudadOrigen,
            CiudadDestino = ciudadDestino,
            PesoKg = pesoKg,
            Estado = EstadoGuia.Pendiente,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void MarcarEntregada()
    {
        Estado = EstadoGuia.Entregada;
        FechaEntrega = DateTime.UtcNow;
    }

    public void ActualizarEstado(string nuevoEstado)
    {
        Estado = nuevoEstado;
    }
}
