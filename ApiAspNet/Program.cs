using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using ApiAspNet.Entities;
using ApiAspNet.Helpers;
using ApiAspNet.Models.Auth;
using ApiAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Serilog;

ConfigureLogging();
//var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    
    Log.Information("Démarrage de l'application");
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        //port 80 exposé dans Docker
        serverOptions.ListenAnyIP(80);
    });
    builder.Host.UseSerilog();
    Log.Information("📢 Ceci est un log de test vers Elasticsearch.");
    ConfigurationManager configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
builder.Services.AddDbContext<DataContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(configuration.GetConnectionString("WebApiDB")));
    builder.Services.AddScoped<IClientService, ClientService>();

    // For Identity 
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Adding Authentication 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer 
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new
TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])) 
    };
});
//builder.Services.AddControllers().AddJsonOptions(x =>

    //logger.Debug("Démarrage de l'application");

    // Intégration de NLog
    //builder.Logging.ClearProviders();
    //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    //builder.Host.UseNLog(); //

    

    // Ajout du contexte DB
    services.AddDbContext<DataContext>();

    // JSON options
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowVueApp", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
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
    app.UseCors("AllowVueApp");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    //logger.Error(ex, "L'application a échoué au démarrage");
    Log.Fatal(ex, "L'application a échoué au démarrage");
    throw;
}
finally
{
    //LogManager.Shutdown();
    Log.CloseAndFlush();
}
static void ConfigureLogging()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
        {
            AutoRegisterTemplate = true,
            IndexFormat = "aspnetcore-logs-{0:yyyy.MM.dd}"
        })
        .CreateLogger();
}





