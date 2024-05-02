namespace Presentation.Controllers.Founder.Dto;

public sealed record UpdateFounderDto(Guid Id, string? Inn, string? FirstName, string? LastName, string? SurName);