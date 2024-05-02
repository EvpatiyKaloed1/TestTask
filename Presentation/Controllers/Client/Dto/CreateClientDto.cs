using Domain.Clients.ValueObjects;

namespace Presentation.Controllers.Client.Dto;

public sealed record CreateClientDto(string Inn, string Name, ClientType Type, IEnumerable<Guid>? Founders);