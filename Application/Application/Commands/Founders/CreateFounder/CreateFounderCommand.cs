using Domain.Common.ValueObjects;
using Domain.Founders;
using Domain.Founders.ValueObjects;
using MediatR;

namespace Application.Commands.Founders.CreateFounder;

public sealed record CreateFounderCommand(Inn Inn, FounderFullName FounderFull) : IRequest<Founder>;