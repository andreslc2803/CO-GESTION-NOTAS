using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Api.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace GestionNotas.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                logger.LogWarning("Errores de validación: {@Errors}", ex.Message);
                await EscribirRespuesta(context, StatusCodes.Status400BadRequest,
                    ApiResponse<object>.Failure(ex.Message));
            }
            catch (BusinessException ex)
            {
                logger.LogWarning("Error de negocio: {Message}", ex.Message);
                await EscribirRespuesta(context, StatusCodes.Status200OK,
                    ApiResponse<object>.Failure(ex.Message));
            }
            catch (DomainException ex)
            {
                logger.LogWarning("Excepción de dominio: {Message}", ex.Message);
                await EscribirRespuesta(context, StatusCodes.Status422UnprocessableEntity,
                    ApiResponse<object>.Failure(ex.Message));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error inesperado al procesar la solicitud.");
                await EscribirRespuesta(context, StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Failure("Ocurrió un error interno. Por favor intente más tarde."));
            }
        }

        private static async Task EscribirRespuesta<T>(HttpContext context, int statusCode, ApiResponse<T> respuesta)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta, JsonOptions));
        }
    }
}
