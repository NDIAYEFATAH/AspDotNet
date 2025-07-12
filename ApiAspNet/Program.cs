// using
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Services;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("Démarrage de l'application");

    var builder = WebApplication.CreateBuilder(args);

    // Intégration de NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp",
            policy => policy.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod());
    });

    var services = builder.Services;

    // 📌 Intégration de Redis
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379"; 
        options.InstanceName = "MyApp_";
    });

    // DB context
    services.AddDbContext<DataContext>();

    // JSON options
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IFlotteService, FlotteService>();
    services.AddScoped<IAgenceService, AgenceService>();
    services.AddScoped<IChauffeurService, ChauffeurService>();
    services.AddScoped<IVoyageService, VoyageService>();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    var app = builder.Build();

    // Test simple Redis (ajoute cet endpoint pour tester)
    app.MapGet("/cache", async (IDistributedCache cache) =>
    {
        string key = "ma_cle";
        var value = await cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(value))
        {
            value = $"Valeur générée à {DateTime.Now}";
            await cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }

        return Results.Ok(new { value });
    });

    // Pipeline HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAngularApp");
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
