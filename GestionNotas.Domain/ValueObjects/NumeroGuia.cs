namespace GestionNotas.Api.Domain.ValueObjects;

public sealed record NumeroGuia
{
    public string Valor { get; }

    public NumeroGuia(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("El número de guía no puede estar vacío.", nameof(valor));

        if (valor.Length < 5 || valor.Length > 20)
            throw new ArgumentException("El número de guía debe tener entre 5 y 20 caracteres.", nameof(valor));

        Valor = valor.ToUpperInvariant();
    }

    public override string ToString() => Valor;

    public static implicit operator string(NumeroGuia numeroGuia) => numeroGuia.Valor;
    public static explicit operator NumeroGuia(string valor) => new(valor);
}
