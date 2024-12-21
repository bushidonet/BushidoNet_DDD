using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Test;

public class ClientServiceTest
{
    private readonly AppDbContext _dbContext;
    private readonly ClientService _clientService;
    private readonly IClientRepository _clientRepository;
    

    public ClientServiceTest()
    {
        // Configurar DbContext para usar base de datos en memoria
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")  // Nombre único para evitar conflictos entre pruebas
            .Options;

        _dbContext = new AppDbContext(options);
        _clientRepository = new ClientRepository(_dbContext);
        _clientService = new ClientService(_clientRepository);  // Suponiendo que tu servicio usa AppDbContext
    }
    
    //DEBERIA: De añadir un cliente
    [Fact]
    public async Task AddClientAsync_ShouldAddClientSuccessfully()
    {
        // Arrange
        var client = new Client { Id = 1, FirstName = "Test Client" };

        // Act
        var result = await _clientService.AddClientAsync(client);

        // Assert
        var savedClient = await _dbContext.Clients.FindAsync(client.Id);
        Assert.NotNull(savedClient);
        Assert.Equal(client.Id, savedClient.Id);
        Assert.Equal(client.FirstName, savedClient.FirstName);
    }
    //DEBERIA: Devolver un cliente
    [Fact]
    public async Task GetClientByIdAsync_ShouldReturnClient()
    {
        // Arrange
        var client = new Client { Id = 2, FirstName = "Existing Client" };
        _dbContext.Clients.Add(client);
        
        // Act
        var result = await _clientService.GetClientByIdAsync(client.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(client.Id, result.Id);
        Assert.Equal(client.FirstName, result.FirstName);
    }
}