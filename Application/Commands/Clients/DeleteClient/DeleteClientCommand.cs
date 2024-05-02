using MediatR;

namespace Application.Commands.Clients.DeleteClient;
public sealed record DeleteClientCommand(Guid Id) : IRequest;