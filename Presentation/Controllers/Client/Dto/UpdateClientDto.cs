using Domain.Clients.ValueObjects;

namespace Presentation.Controllers.Client.Dto;

public sealed record UpdateClientDto(Guid Id,
                                     string? Inn,
                                     ClientType? Type,
                                     Guid Founder,
                                     string? Name);