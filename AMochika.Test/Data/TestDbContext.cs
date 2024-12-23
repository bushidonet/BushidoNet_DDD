using AMochika.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Test.Data;

public class Seed
{

}
//
// public class TestDbContext : DbContext
// {
//     public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
//
//     public DbSet<Client> Clients { get; set; }
// }
//
// private AppDbContext CreateInMemoryDbContext()
// {
//     var options = new DbContextOptionsBuilder<AppDbContext>()
//         .UseInMemoryDatabase("TestDatabase") // Nombre único para la base de datos
//         .Options;
//
//     return new AppDbContext(options);
// }