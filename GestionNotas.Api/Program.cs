using GestionNotas.Api.Api.Extensions;
using GestionNotas.Api.Middleware;
using GestionNotas.Api.Infrastructure.Extensions;
using GestionNotas.Api.Application.Extensions; ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
app.MapControllers();
app.MapSwaggerConfiguration();
app.UseOpenApiFileExport();

app.Run();
