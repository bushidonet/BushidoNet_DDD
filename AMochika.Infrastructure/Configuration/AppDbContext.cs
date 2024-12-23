using AMochika.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Infrastructure.Configuration;

public class AppDbContext: DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<MedicalHistory> MedicalHistories { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Seeding.Seed(modelBuilder);
        
        // La clave primaria es una combinación de PurchaseId y ProductId
        modelBuilder.Entity<PurchaseDetail>()
            .HasKey(cp => new { cp.PurchaseId, cp.ProductId });  
        
        modelBuilder.Entity<PurchaseDetail>()
            .HasOne(cp => cp.Purchase)
            .WithMany(c => c.PurchaseDetails)
            .HasForeignKey(cp => cp.PurchaseId);
        
        modelBuilder.Entity<PurchaseDetail>()
            .HasOne(cp => cp.Product)
            .WithMany(c => c.PurchaseDetails)
            .HasForeignKey(cp => cp.ProductId);

        // Configuración de la entidad Client
        modelBuilder.Entity<Client>()
            .ToTable("Clients")  // Mapea la entidad a la tabla "Clients"
            .HasKey(c => c.Id);  // Configura "Id" como clave primaria

        // Configuración de las relaciones
        modelBuilder.Entity<Client>()
            .HasMany(c => c.MedicalHistories)  // Relación uno a muchos con MedicalHistories
            .WithOne(m => m.Client)  // Cada MedicalHistory tiene un Client asociado
            .HasForeignKey(m => m.ClientId); // Configura la clave foránea ClientId en MedicalHistory

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Appointments)  // Relación uno a muchos con Appointments
            .WithOne(a => a.Client)  // Cada Appointment tiene un Client asociado
            .HasForeignKey(a => a.ClientId); // Configura la clave foránea ClientId en Appointment

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Purchases)  // Relación uno a muchos con Purchases
            .WithOne(p => p.Client)  // Cada Purchase tiene un Client asociado
            .HasForeignKey(p => p.ClientId); // Configura la clave foránea ClientId en Purchase

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Recommendations)  // Relación uno a muchos con Recommendations
            .WithOne(r => r.Client)  // Cada Recommendation tiene un Client asociado
            .HasForeignKey(r => r.ClientId); // Configura la clave foránea ClientId en Recommendation

    }
    
}