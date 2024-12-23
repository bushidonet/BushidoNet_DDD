namespace AMochika.Core.Entities;
public class Appointment
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; }
    public string Notes { get; set; }

    // Relationship with Client
    public Client Client { get; set; }
}
