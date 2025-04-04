using BlazorApp1;
using BlazorApp1.Components;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Agregar servicios de autenticación y autorización
builder.Services.AddAuthentication(/* Opciones de autenticación */)
    .AddCookie(/* Opciones de cookie */);

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});

builder.Services.AddAuthorizationCore(); // Habilitar autenticación en Blazor
builder.Services.AddBlazoredLocalStorage(); // Registrar Blazored.LocalStoragef
builder.Services.AddSingleton(sp => 
    new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]) });


builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8080/") });
//builder.Services.AddScoped<AuthService>();

//Para registrar las dos clases AuthenticationStateProvider y ILoginService como estan en la misma clase CustomAuthenticationStateProvider
//Debemos registrar primero la clase CustomAuthenticationStateProvider y la reutilizamospara los dos casos.
builder.Services.AddScoped<CustomAuthenticationStateProvider>();

//Registramos AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(provider => 
    provider.GetRequiredService<CustomAuthenticationStateProvider>());

//Registramos ILoginService
builder.Services.AddScoped<ILoginService, CustomAuthenticationStateProvider>(provider => 
    provider.GetRequiredService<CustomAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();