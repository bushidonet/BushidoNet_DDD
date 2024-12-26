using System.Text.Json.Serialization;
using AMochika.Application;
using AMochika.Application.Interfaces;
using AMochika.Application.Mapping;
using AMochika.Application.Services;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno desde el archivo .env
Env.Load();
// Configuración de la aplicación
builder.Configuration.AddEnvironmentVariables();

//>>CONEXION BASE DE DATOS
string DefaultConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");

// Configura el DbContext con SQL Server 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(DefaultConnection)
           .EnableSensitiveDataLogging()
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)
           ));// Habilitar Sensitive Data Logging para mostrar información detallada

// SWAGGER:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CONTROLLER:
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        // Configuración para evitar el ciclo de referencias
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

        // Configura la profundidad máxima de serialización si es necesario
        options.JsonSerializerOptions.MaxDepth = 32;  // Puedes ajustar este valor si es necesario
    });

// SERVICIOS:
//builder.Services.AddScoped<ClientAppService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// SERVICIOS AUXILIARES:
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHealthChecks();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
    });

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita el servicio de archivos estáticos
app.UseRouting();
app.UseDeveloperExceptionPage();
app.UseHsts();
app.MapHealthChecks("/health");
app.Run();