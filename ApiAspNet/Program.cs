using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Services;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    logger.Debug("Démarrage de l'application");

    var builder = WebApplication.CreateBuilder(args);

    // Intégration de NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog(); // <- très important

    var services = builder.Services;

    // Ajout du contexte DB
    services.AddDbContext<DataContext>();

    // JSON options
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Dépendances
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IFlotteService, FlotteService>();
    // services.AddScoped<IVoyageService, VoyageService>();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    var app = builder.Build();

    // Pipeline HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
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
