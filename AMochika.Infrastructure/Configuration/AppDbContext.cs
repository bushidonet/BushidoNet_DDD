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
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Relación muchos a muchos
        
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

    }
    
}