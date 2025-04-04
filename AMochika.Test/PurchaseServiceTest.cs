using AMochika.Application.DTOs;
using AMochika.Application.Mapping;
using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Test;

public class PurchaseServiceTest
{
    private readonly AppDbContext _dbContext;
    private readonly PurchaseService _purchaseService;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMapper _mapper;


    public PurchaseServiceTest()
    {
        // Configurar DbContext para usar base de datos en memoria
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Nombre único para evitar conflictos entre pruebas
            .Options;
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Importa tu perfil de automapper
        });

        _mapper = config.CreateMapper();

        _dbContext = new AppDbContext(options);
        _purchaseRepository = new PurchaseRepository(_dbContext);
        _purchaseService = new PurchaseService(_purchaseRepository);  // Suponiendo que tu servicio usa AppDbContext
    }

    [Fact]
    public async Task GetPurchaseByClientIdAsync_ReturnsPurchases_WhenClientIdIsValid()
    {
        // Arrange: Insertar datos en la base de datos en memoria
        var clientId = 1;
        var purchase1 = new Purchase { ClientId = clientId, Date = DateTime.Now, TotalAmount = 100, IsDeleted = false };
        var purchase2 = new Purchase { ClientId = clientId, Date = DateTime.Now.AddDays(-1), TotalAmount = 200, IsDeleted = false };
        
        _dbContext.Purchases.Add(purchase1);
        _dbContext.Purchases.Add(purchase2);
        await _dbContext.SaveChangesAsync();

        // Act: Llamar al método de servicio
        var result = await _purchaseService.GetPurchaseByClientIdAsync(clientId);

        // Assert: Verificar los resultados
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());  // Esperamos 2 compras para el cliente con clientId 1
    }
    
    [Fact]
    public async Task GetPurchaseByClientIdAsync_ReturnsEmpty_WhenClientHasNoPurchases()
    {
        // Arrange: Cliente sin compras
        var clientId = 2;

        // Act: Llamar al método de servicio
        var result = await _purchaseService.GetPurchaseByClientIdAsync(clientId);

        // Assert: Verificar que no haya compras para ese cliente
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPurchaseByClientIdAsync_DoesNotReturnDeletedPurchases()
    {
        // Arrange: Insertar una compra eliminada
        var clientId = 1;
        var purchase1 = new Purchase { ClientId = clientId, Date = DateTime.Now, TotalAmount = 100, IsDeleted = true };
        _dbContext.Purchases.Add(purchase1);
        await _dbContext.SaveChangesAsync();

        // Act: Llamar al método de servicio
        var result = await _purchaseService.GetPurchaseByClientIdAsync(clientId);

        // Assert: Verificar que la compra eliminada no aparece en los resultados
        Assert.Empty(result); // No debe devolver compras eliminadas
    }
    
    [Fact]
    public async Task AddPurchaseAsync_SuccessfullyAddsPurchase()
    {
        // Arrange: Crear una nueva compra
        var purchase = new Purchase { ClientId = 1, Date = DateTime.Now, TotalAmount = 500, IsDeleted = false };

        // Act: Agregar la compra al repositorio
        await _purchaseService.AddAsync(purchase);

        // Assert: Verificar que la compra fue agregada
        var savedPurchase = await _dbContext.Purchases.FindAsync(purchase.Id);
        Assert.NotNull(savedPurchase);
        Assert.Equal(500, savedPurchase.TotalAmount);
        Assert.Equal(1, savedPurchase.ClientId);
    }
 }