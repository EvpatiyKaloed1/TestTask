using Domain.Clients.ValueObjects;

namespace Infrastructure.Dto;

public sealed class ClientDto
{
    public Guid Id { get; set; }
    public ClientType Type { get; set; }
    public string Name { get; set; }
    public string Inn { get; set; }
    public List<FounderDto>? Founders { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}