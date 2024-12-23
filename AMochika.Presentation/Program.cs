using AMochika.Application;
using AMochika.Application.Interfaces;
using AMochika.Application.Mapping;
using AMochika.Application.Services;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno desde el archivo .env
Env.Load();

// Configuración de la aplicación
builder.Configuration.AddEnvironmentVariables();

string DefaultConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");

// Configura el DbContext con SQL Server 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(DefaultConnection
    ).EnableSensitiveDataLogging()); // Habilitar Sensitive Data Logging para mostrar información detallada

// Agregar Swagger al contenedor de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// SERVICIOS:
//builder.Services.AddScoped<ClientAppService>();
builder.Services.AddScoped<ClientService>();
// Registrar repositorios
builder.Services.AddScoped<IClientRepository, ClientRepository>();
// Registrar servicios
//builder.Services.AddScoped<ClientService1>();
// Registrar repositorios en el contenedor de dependencias
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHealthChecks();

var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
    });
    
app.MapHealthChecks("/health");

app.UseDeveloperExceptionPage();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();