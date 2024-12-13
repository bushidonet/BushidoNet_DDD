namespace AMochika.Core.Entities;

public class PurchaseDetail
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Relationship with Purchase
    public Purchase Purchase { get; set; }

    // Relationship with Product
    public Product Product { get; set; }
}