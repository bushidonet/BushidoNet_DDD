namespace AMochika.Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    // Relationship with PurchaseDetails
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; }

    // Relationship with Recommendations
    public ICollection<Recommendation> Recommendations { get; set; }
}