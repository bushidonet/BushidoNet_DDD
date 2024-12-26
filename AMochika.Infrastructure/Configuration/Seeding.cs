using AMochika.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Infrastructure.Configuration;

public class Seeding
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var clients = new[]
        {
            new Client { Id = 1, FirstName = "Juan", LastName = "Pérez", Email = "juan.perez@gmail.com", Phone = "555-123-4567", BirthDate = new DateTime(1990, 5, 12) },
            new Client { Id = 2, FirstName = "María", LastName = "Gómez", Email = "maria.gomez@hotmail.com", Phone = "555-987-6543", BirthDate = new DateTime(1985, 3, 22) },
            new Client { Id = 3, FirstName = "Carlos", LastName = "Rodríguez", Email = "carlos.rodriguez@yahoo.com", Phone = "555-234-5678", BirthDate = new DateTime(1992, 7, 19) },
            new Client { Id = 4, FirstName = "Ana", LastName = "Martínez", Email = "ana.martinez@outlook.com", Phone = "555-345-6789", BirthDate = new DateTime(1988, 11, 30) },
            new Client { Id = 5, FirstName = "Luis", LastName = "Hernández", Email = "luis.hernandez@gmail.com", Phone = "555-456-7890", BirthDate = new DateTime(1995, 2, 14) },
            new Client { Id = 6, FirstName = "Laura", LastName = "Sánchez", Email = "laura.sanchez@gmail.com", Phone = "555-567-8901", BirthDate = new DateTime(1987, 8, 9) },
            new Client { Id = 7, FirstName = "José", LastName = "Torres", Email = "jose.torres@gmail.com", Phone = "555-678-9012", BirthDate = new DateTime(1993, 1, 23) },
            new Client { Id = 8, FirstName = "Sofía", LastName = "Díaz", Email = "sofia.diaz@yahoo.com", Phone = "555-789-0123", BirthDate = new DateTime(1991, 6, 18) },
            new Client { Id = 9, FirstName = "Miguel", LastName = "Ruiz", Email = "miguel.ruiz@outlook.com", Phone = "555-890-1234", BirthDate = new DateTime(1989, 10, 5) },
            new Client { Id = 10, FirstName = "Camila", LastName = "López", Email = "camila.lopez@hotmail.com", Phone = "555-901-2345", BirthDate = new DateTime(1994, 4, 25) }
            
        };
        
        modelBuilder.Entity<Client>().HasData(clients);
        
        // Seeding Purchases
        var random = new Random();
        var purchases = new List<Purchase>();
        for (int i = 1; i <= 20; i++)
        {
            var client = clients[random.Next(clients.Length)];
            purchases.Add(new Purchase
            {
                Id = i,
                ClientId = client.Id,
                Date = DateTime.Now.AddDays(-random.Next(1, 31)),
                TotalAmount = Math.Round((decimal)(random.NextDouble() * 1000 + 50), 2),
                IsDeleted = false
            });
        }

        modelBuilder.Entity<Purchase>().HasData(purchases);
        
        // Seeding de Productos (10 productos)
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product A", Description = "Description of Product A", Price = 100.00m },
            new Product { Id = 2, Name = "Product B", Description = "Description of Product B", Price = 200.00m },
            new Product { Id = 3, Name = "Product C", Description = "Description of Product C", Price = 150.00m },
            new Product { Id = 4, Name = "Product D", Description = "Description of Product D", Price = 120.00m },
            new Product { Id = 5, Name = "Product E", Description = "Description of Product E", Price = 250.00m },
            new Product { Id = 6, Name = "Product F", Description = "Description of Product F", Price = 180.00m },
            new Product { Id = 7, Name = "Product G", Description = "Description of Product G", Price = 220.00m },
            new Product { Id = 8, Name = "Product H", Description = "Description of Product H", Price = 300.00m },
            new Product { Id = 9, Name = "Product I", Description = "Description of Product I", Price = 350.00m },
            new Product { Id = 10, Name = "Product J", Description = "Description of Product J", Price = 400.00m }
        );
        
        // Seeding de PurchaseDetails (detalles de la compra)
        modelBuilder.Entity<PurchaseDetail>().HasData(
            new PurchaseDetail
            {
                Id = 1,
                ProductId = 1,
                PurchaseId = 1,
                Quantity = 2,
                UnitPrice = 100.00m
            },
            new PurchaseDetail
            {
                Id = 2,
                ProductId = 2,
                PurchaseId = 2,
                Quantity = 1,
                UnitPrice = 200.00m
            },
            new PurchaseDetail
            {
                Id = 3,
                ProductId = 3,
                PurchaseId = 1,
                Quantity = 3,
                UnitPrice = 150.00m
            },
            new PurchaseDetail
            {
                Id = 4,
                ProductId = 4,
                PurchaseId = 2,
                Quantity = 1,
                UnitPrice = 120.00m
            },
            new PurchaseDetail
            {
                Id = 5,
                ProductId = 5,
                PurchaseId = 1,
                Quantity = 2,
                UnitPrice = 250.00m
            },
            new PurchaseDetail
            {
                Id = 6,
                ProductId = 6,
                PurchaseId = 2,
                Quantity = 1,
                UnitPrice = 180.00m
            },
            new PurchaseDetail
            {
                Id = 7,
                ProductId = 7,
                PurchaseId = 1,
                Quantity = 1,
                UnitPrice = 220.00m
            },
            new PurchaseDetail
            {
                Id = 8,
                ProductId = 8,
                PurchaseId = 2,
                Quantity = 1,
                UnitPrice = 300.00m
            },
            new PurchaseDetail
            {
                Id = 9,
                ProductId = 9,
                PurchaseId = 1,
                Quantity = 1,
                UnitPrice = 350.00m
            },
            new PurchaseDetail
            {
                Id = 10,
                ProductId = 10,
                PurchaseId = 2,
                Quantity = 1,
                UnitPrice = 400.00m
            }
        );
        
        // Seeding de Recomendaciones
        modelBuilder.Entity<Recommendation>().HasData(
            new Recommendation
            {
                Id = 1,
                ClientId = 1,
                ProductId = 1,
                RecommendationDate = DateTime.Now,
                Reason = "Highly recommended product!"
            },
            new Recommendation
            {
                Id = 2,
                ClientId = 2,
                ProductId = 2,
                RecommendationDate = DateTime.Now,
                Reason = "Great value for money!"
            },
            new Recommendation
            {
                Id = 3,
                ClientId = 1,
                ProductId = 5,
                RecommendationDate = DateTime.Now,
                Reason = "Best choice for premium customers!"
            },
            new Recommendation
            {
                Id = 4,
                ClientId = 2,
                ProductId = 7,
                RecommendationDate = DateTime.Now,
                Reason = "Excellent durability!"
            },
            new Recommendation
            {
                Id = 5,
                ClientId = 1,
                ProductId = 10,
                RecommendationDate = DateTime.Now,
                Reason = "Amazing quality and performance!"
            }
        );
    }
}