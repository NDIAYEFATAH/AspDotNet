using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("Démarrage de l'application");

    var builder = WebApplication.CreateBuilder(args);

    // Intégration de NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var services = builder.Services;

    // Ajout du contexte DB
    services.AddDbContext<DataContext>();

    // Configuration CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp",
            policy => policy
                .WithOrigins("http://localhost:4200") // URL de ton app Angular
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    // Configuration JSON
    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // Configuration Authentification JWT avec Keycloak
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "http://localhost:8080/realms/api-realm"; // URL Keycloak + realm
            options.Audience = "aspnet-api"; // Client ID Keycloak
            options.RequireHttpsMetadata = false; // En local uniquement

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8080/realms/api-realm",
                ValidateAudience = true,
                ValidAudience = "aspnet-api",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // optionnel : réduit la tolérance temps sur expiration
            };
        });

    // Injection des services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IFlotteService, FlotteService>();
    services.AddScoped<IAgenceService, AgenceService>();

    // AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Swagger sécurisé avec support JWT Bearer
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Exemple: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    // Configure l'URL d'écoute (docker friendly)
    builder.WebHost.UseUrls("http://0.0.0.0:80");

    var app = builder.Build();

    // Middleware HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAngularApp");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "L'application a échoué au démarrage");
    throw;
}
finally
{
    LogManager.Shutdown();
}
