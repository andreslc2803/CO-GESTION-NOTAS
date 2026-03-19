using GestionNotas.Api.Application.Common.Settings;
using GestionNotas.Api.Infrastructure.Persistence;
using GestionNotas.Api.Infrastructure.Settings;
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

        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        services.AddDbContext<ServiciosDbContext>((sp, options) =>
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


        services.AddScoped<IAppSettingsPolicy, AppSettingsPolicy>();

        return services;
    }
}