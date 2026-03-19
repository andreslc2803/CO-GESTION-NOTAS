using GestionNotas.Api.Infrastructure.Persistence;
using GestionNotas.Domain.Interfaces.Repositories;
using GestionNotas.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestionNotas.Api.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        //services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        services.AddDbContext<GestionNotasDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("ConnectionBd") ?? "";

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);

                sqlOptions.CommandTimeout(30);
            });
        });

        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        services.AddScoped<INotaRepository, NotaRepository>();
        services.AddScoped<IProfesorRepository, IProfesorRepository>();

        return services;
    }
}