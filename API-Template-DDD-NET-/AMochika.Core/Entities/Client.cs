namespace AMochika.Core.Entities;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }

    // Relationships
    public ICollection<MedicalHistory> MedicalHistories { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Purchase> Purchases { get; set; }
    public ICollection<Recommendation> Recommendations { get; set; }
}
