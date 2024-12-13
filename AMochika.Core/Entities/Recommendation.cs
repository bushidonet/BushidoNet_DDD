namespace AMochika.Core.Entities;

public class Recommendation
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int ProductId { get; set; }
    public DateTime RecommendationDate { get; set; }
    public string Reason { get; set; }

    // Relationship with Client
    public Client Client { get; set; }

    // Relationship with Product
    public Product Product { get; set; }
}