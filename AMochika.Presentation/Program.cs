using System.Text;
using System.Text.Json.Serialization;
using AMochika.Application;
using AMochika.Application.Interfaces;
using AMochika.Application.Mapping;
using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddDbContext<AppDbContext>(options =>
    //options.UseSqlServer("Server=localhost;Database=master;User Id=su;Password=admin2025..;MultipleActiveResultSets=True;")
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
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

        // Configura la profundidad máxima de serialización si es necesario
        options.JsonSerializerOptions.MaxDepth = 32;  // Puedes ajustar este valor si es necesario
    });

// SERVICIOS:
//Register:
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// SERVICIOS AUXILIARES:
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHealthChecks();

//KESTREL
builder.WebHost.ConfigureKestrel(options =>
{
    logger.Info("Solicitud recibida en la raíz");
    options.ListenAnyIP(80);
});

//LOG CONFIGURATION:  NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//         policy =>
//         {
//             policy.WithOrigins("http://localhost:5173") // URL de React
//                 .AllowAnyMethod()
//                 .AllowAnyHeader();
//         });
// });


// Agrega servicios de autenticación y autorización
builder.WebHost.UseUrls("http://*:80");
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });

    // Definición del esquema Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",          // <- en minúsculas
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce: Bearer {superclaveultrasecreta1234567890}"
    });

    // Requerimiento global (aplica a todas las operaciones)
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
            Array.Empty<string>()
        }
    });
});






var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
    });
   

//APPLY MIGRATIONS
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated(); // Crear la base de datos en memoria
            // Ejecutar el seed
        
            Console.WriteLine("----------------------------------------MIGRATE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("----------------------------------------MIGRATE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            
            logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");logger.Error( "An error occurred while migrating the database.");
        }
        catch (Exception ex)
        {
        
            logger.Error(ex, "An error occurred while migrating the database.");
        }
    }

//MIDDLEWARE:
// app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita el servicio de archivos estáticos
app.UseRouting();
app.UseDeveloperExceptionPage();
app.UseHsts();
app.MapHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();
app.Run();