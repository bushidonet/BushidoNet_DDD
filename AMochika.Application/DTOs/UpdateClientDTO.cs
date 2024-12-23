namespace AMochika.Application.DTOs;

public record UpdateClientDTO
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime BirthDate { get; init; }
}