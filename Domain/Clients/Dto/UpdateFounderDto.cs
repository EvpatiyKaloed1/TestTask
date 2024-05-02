using Domain.Common.ValueObjects;

namespace Domain.Clients.Dto;

public readonly record struct UpdateFounderDto(Inn? Inn, string? FirstName, string? LastName, string? SurName);