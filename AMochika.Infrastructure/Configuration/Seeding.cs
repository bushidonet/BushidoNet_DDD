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
    }
}