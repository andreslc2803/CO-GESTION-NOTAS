using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using GestionNotas.Api.Application.Common.Behaviors;

namespace GestionNotas.Api.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceExtensions).Assembly));

        services.AddValidatorsFromAssembly(typeof(ApplicationServiceExtensions).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
