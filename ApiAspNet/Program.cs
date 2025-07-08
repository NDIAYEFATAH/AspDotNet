using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
                .WithOrigins("http://localhost:4200") // URL  app Angular
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    // Configuration JSON
    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // JWT - Authentification
    var key = builder.Configuration["Jwt:Key"];
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });


    // AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // ?? Dépendances à injecter
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IFlotteService, FlotteService>();
    services.AddScoped<IAgenceService, AgenceService>(); // ? Ajouté ici
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

    // ?? CORS doit être ici AVANT authorization/controllers
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
