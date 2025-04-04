using AMochika.Infrastructure.Configuration.Auth;
using Blazored.LocalStorage;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); 


var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

// Cargar las variables de entorno desde el archivo .env
Env.Load();
logger.Info("archivo .env cargado");

// Configuración de la aplicación
builder.Configuration.AddEnvironmentVariables();
//>>CONEXION BASE DE DATOS
string DefaultConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");

//DATABASE IN MEMORY
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("InMemoryDb") // Usar base de datos en memoria
//         .EnableSensitiveDataLogging()      // Habilitar Sensitive Data Logging
//         .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
// );

//Configura el DbContext con SQL Server 
builder.Services.AddDbContext<AuthDbContext>(options =>
    //options.UseSqlServer("Server=localhost;Database=master;User Id=su;Password=admin2025..;MultipleActiveResultSets=True;")
    options.UseSqlServer(DefaultConnection)
        .EnableSensitiveDataLogging()
        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)
        ));// Habilitar Sensitive Data Logging para mostrar información detallada

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// SWAGGER:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//KESTREL
builder.WebHost.ConfigureKestrel(options =>
{
    logger.Info("Solicitud recibida en la raíz");
    options.ListenAnyIP(5078);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
});
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    dbContext.Database.EnsureCreated();
}
app.MapControllers();
app.Run();
