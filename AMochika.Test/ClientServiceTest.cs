using AMochika.Application.DTOs;
using AMochika.Application.Mapping;
using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Test;

// public class ClientServiceTest
// {
//     private readonly AppDbContext _dbContext;
//     private readonly ClientService _clientService;
//     private readonly IClientRepository _clientRepository;
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IMapper _mapper;
//

    // public ClientServiceTest()
    // {
    //     // Configurar DbContext para usar base de datos en memoria
    //     var options = new DbContextOptionsBuilder<AppDbContext>()
    //         .UseInMemoryDatabase("TestDatabase")  // Nombre único para evitar conflictos entre pruebas
    //         .Options;
    //     
    //     var config = new MapperConfiguration(cfg =>
    //     {
    //         cfg.AddProfile<MappingProfile>(); // Importa tu perfil
    //     });
    //
    //     _mapper = config.CreateMapper();
    //
    //     _dbContext = new AppDbContext(options);
    //     _clientRepository = new ClientRepository(_dbContext);
    //     _unitOfWork = new UnitOfWork(_dbContext);
    //     _clientService = new ClientService(_clientRepository, _unitOfWork,_mapper);  // Suponiendo que tu servicio usa AppDbContext
    // }

    //DEBERIA: De añadir un cliente
    // [Fact]
    // public async Task AddClientAsync_ShouldAddClientSuccessfully()
    // {
    //     // Arrange
    //     var client =  new CreateClientDTO
    //     {
    //         FirstName = "John",
    //         LastName = "Doe",
    //         Email = "johndoe@example.com",
    //         Phone = "+123456789",
    //         BirthDate = new DateTime(1990, 1, 1) // Fecha de nacimiento
    //     };
    //     // Act
    //     var result = await _clientService.AddClientAsync(client);
    //     // Assert
    //     // Usar FirstOrDefaultAsync para buscar con condiciones personalizadas
    //     var savedClient = await _dbContext.Clients
    //         .FirstOrDefaultAsync(x => x.FirstName == "John" && x.LastName == "Doe");
    //
    //     Assert.NotNull(savedClient);
    //     Assert.Equal(result, savedClient.Id);  // Asegúrate de que el Id se retorna y es correcto
    //     Assert.Equal(client.FirstName, savedClient.FirstName);
    //     Assert.Equal(client.LastName, savedClient.LastName);
    //     Assert.Equal(client.Email, savedClient.Email);
    //     Assert.Equal(client.Phone, savedClient.Phone);
    //     Assert.Equal(client.BirthDate, savedClient.BirthDate);
    // }
 //         // Arrange
 //         var client = new Client { Id = 2, FirstName = "Existing Client" };
 //         _dbContext.Clients.Add(client);
 //
 //         // Act
 //         var result = await _clientService.GetClientByIdAsync(client.Id);
 //
 //         // Assert
 //         Assert.NotNull(result);
 //         Assert.Equal(client.Id, 2);
 //         Assert.Equal(client.FirstName, result.FirstName);
 //     }
 // }