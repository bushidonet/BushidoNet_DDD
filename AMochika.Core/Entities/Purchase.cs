namespace AMochika.Core.Entities;

public class Purchase //Compra
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsDeleted { get; set; }

    // Relationship with Client
    public Client Client { get; set; }

    // Relationship with PurchaseDetails
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
}