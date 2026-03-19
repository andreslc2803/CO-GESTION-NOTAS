using System.Text.Json;
using System.Text.Json.Nodes;
using Scalar.AspNetCore;

namespace GestionNotas.Api.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, ct) =>
            {
                document.Info = new()
                {
                    Title = "Api Servicios GreenMobile .NET 10",
                    Version = "v1",
                    Description = "API para la gestion de distribucion y recoleccion en GreenMobile"
                };
                document.Servers?.Clear();
                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseRouting();
        return app;
    }

    public static IEndpointRouteBuilder MapSwaggerConfiguration(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapOpenApi();
        endpoints.MapScalarApiReference(options =>
        {
            options.Title = "Servientrega Servicios GreenMobile - API";
            options.Theme = ScalarTheme.Purple;
            options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        return endpoints;
    }

    public static WebApplication UseOpenApiFileExport(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    var baseUrl = app.Urls
                        .FirstOrDefault(u => u.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        ?? app.Urls.FirstOrDefault()
                        ?? "http://localhost:5000";

                    using var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                    var json = await client.GetStringAsync("/openapi/v1.json");

                    var json30 = ConvertToOpenApi30(json);

                    var webRoot = app.Environment.WebRootPath
                        ?? Path.Combine(app.Environment.ContentRootPath, "wwwroot");

                    Directory.CreateDirectory(webRoot);

                    var destino = Path.Combine(webRoot, "swagger.json");
                    await File.WriteAllTextAsync(destino, json30);

                    app.Logger.LogInformation(
                        "swagger.json (OpenAPI 3.0) exportado para APIM en: {Path}", destino);
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Error al exportar swagger.json para APIM.");
                }
            });
        });

        return app;
    }

    /// <summary>
    /// Convierte un documento OpenAPI 3.1.x a 3.0.x para compatibilidad con APIM.
    /// Principales diferencias manejadas:
    /// - Versión: 3.1.x → 3.0.3
    /// - Type arrays con null: ["null","string"] → type:"string" + nullable:true
    /// - oneOf con null: oneOf:[{type:null},{$ref}] → $ref + nullable:true
    /// - pattern en tipos no-string: se elimina (solo válido en string en 3.0)
    /// </summary>
    private static string ConvertToOpenApi30(string openApi31Json)
    {
        var root = JsonNode.Parse(openApi31Json)!.AsObject();

        root["openapi"] = "3.0.3";

        if (root["components"]?["schemas"] is JsonObject schemas)
            foreach (var key in schemas.Select(s => s.Key).ToList())
                if (schemas[key] is JsonObject schema)
                    FixSchemaForOpenApi30(schema);

        FixPathsForOpenApi30(root["paths"]?.AsObject());

        return root.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
    }

    private static void FixPathsForOpenApi30(JsonObject? paths)
    {
        if (paths is null) return;

        foreach (var pathItem in paths.Select(p => p.Value?.AsObject()).OfType<JsonObject>())
        foreach (var operation in pathItem.Select(m => m.Value?.AsObject()).OfType<JsonObject>())
        {
            if (operation["requestBody"]?["content"] is JsonObject reqContent)
                foreach (var mediaType in reqContent.Select(m => m.Value?.AsObject()).OfType<JsonObject>())
                    if (mediaType["schema"] is JsonObject s) FixSchemaForOpenApi30(s);

            if (operation["responses"] is JsonObject responses)
                foreach (var response in responses.Select(r => r.Value?.AsObject()).OfType<JsonObject>())
                    if (response["content"] is JsonObject respContent)
                        foreach (var mediaType in respContent.Select(m => m.Value?.AsObject()).OfType<JsonObject>())
                            if (mediaType["schema"] is JsonObject s) FixSchemaForOpenApi30(s);
        }
    }

    private static void FixSchemaForOpenApi30(JsonObject schema)
    {
        // oneOf:[{type:"null"},{$ref}] → $ref directo + nullable:true
        if (schema["oneOf"] is JsonArray oneOf)
        {
            var nonNullItems = oneOf
                .OfType<JsonObject>()
                .Where(x => x["type"]?.GetValue<string>() != "null")
                .ToList();
            var hasNull = oneOf.Count != nonNullItems.Count;

            if (hasNull && nonNullItems.Count == 1)
            {
                schema.Remove("oneOf");
                if (hasNull) schema["nullable"] = true;
                foreach (var prop in nonNullItems[0].ToList())
                    schema[prop.Key] = prop.Value?.DeepClone();
            }
        }

        // type:[...] → type único + nullable:true cuando corresponde
        if (schema["type"] is JsonArray typeArray)
        {
            var types = typeArray.Select(t => t?.GetValue<string>()).ToList();
            var hasNull = types.Contains("null");
            var nonNullTypes = types.Where(t => t != "null").ToList();

            schema.Remove("type");

            if (nonNullTypes.Count >= 1)
            {
                // Para ["number","string"] o ["integer","string"] preferimos el tipo nativo
                var primaryType = nonNullTypes.FirstOrDefault(t => t != "string") ?? nonNullTypes[0];
                schema["type"] = primaryType;

                // pattern solo es válido en string en OpenAPI 3.0
                if (primaryType != "string")
                    schema.Remove("pattern");
            }

            if (hasNull) schema["nullable"] = true;
        }

        // Recursividad: properties
        if (schema["properties"] is JsonObject props)
            foreach (var key in props.Select(p => p.Key).ToList())
                if (props[key] is JsonObject propSchema)
                    FixSchemaForOpenApi30(propSchema);

        // Recursividad: items (arrays)
        if (schema["items"] is JsonObject items)
            FixSchemaForOpenApi30(items);

        // Recursividad: allOf / anyOf / oneOf restantes
        foreach (var combiner in new[] { "allOf", "anyOf", "oneOf" })
            if (schema[combiner] is JsonArray arr)
                foreach (var item in arr.OfType<JsonObject>())
                    FixSchemaForOpenApi30(item);
    }
}
