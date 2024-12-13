using AMochika.Application;
using AMochika.Core.Interfaces;
using AMochika.Core.Services;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Configura el DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ).EnableSensitiveDataLogging()); // Habilitar Sensitive Data Logging para mostrar información detallada

// Agregar Swagger al contenedor de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Registra IClientAppService como un servicio
builder.Services.AddScoped<ClientAppService>();
// Registrar repositorios
builder.Services.AddScoped<IClientRepository, ClientRepository>();
// Registrar servicios
builder.Services.AddScoped<ClientService>();
// Registrar repositorios en el contenedor de dependencias
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
    });
}

app.MapControllers(); 

app.UseHttpsRedirection();

app.Run();

