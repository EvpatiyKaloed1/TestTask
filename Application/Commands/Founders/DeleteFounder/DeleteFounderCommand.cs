using MediatR;

namespace Application.Commands.Founders.DeleteFounder;
public sealed record DeleteFounderCommand(Guid Id) : IRequest;