using Domain.Common.ValueObjects;
using Domain.Founders;
using MediatR;

namespace Application.Commands.Founders.UpdateFounder;
public sealed record UpdateFounderCommand(Guid Id, Inn? Inn, string? FirstName, string? LastName, string? SurName) : IRequest<Founder>;