namespace Infrastructure.Dto;

public class FounderDto
{
    public string Inn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SurName { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public ClientDto Client { get; set; }
    public Guid Id { get; set; }
}