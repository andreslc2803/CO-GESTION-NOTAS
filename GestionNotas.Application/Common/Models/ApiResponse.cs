namespace GestionNotas.Api.Application.Common.Models;

public class ApiResponse<T>
{
    public bool Exitoso { get; init; }
    public string Mensaje { get; init; } = string.Empty;
    public T? Data { get; init; }
    public IEnumerable<string> Errores { get; init; } = [];

    public static ApiResponse<T> Success(T data, string mensaje = "Operación exitosa") =>
        new() { Exitoso = true, Mensaje = mensaje, Data = data };

    public static ApiResponse<T> Failure(IEnumerable<string> errores, string mensaje = "La solicitud contiene errores") =>
        new() { Exitoso = false, Mensaje = mensaje, Errores = errores };

    public static ApiResponse<T> Failure(string error) =>
        Failure([error]);
}
